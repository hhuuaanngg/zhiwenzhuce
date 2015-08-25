using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;
using System.Data.SqlClient;


namespace Sample
{
    public partial class MainFrm : Form
    {
        bool gConnected = false;
        byte[] g_FPBuffer;
        int g_FPBufferSize = 0;
        bool g_bIsTimeToDie = false;
        IntPtr g_Handle = IntPtr.Zero;
        IntPtr g_biokeyHandle = IntPtr.Zero;
        IntPtr g_FormHandle = IntPtr.Zero;
        int g_nWidth = 0;
        int g_nHeight = 0;
        bool g_IsRegister = false;
        int g_RegisterTimeCount = 0;
        int g_RegisterCount = 0;
        const int REGISTER_FINGER_COUNT = 3;
        bool g_IfExistID = false;

        byte[][] g_RegTmps = new byte[3][];
	    byte[] g_RegTmp = new byte[2048];
        byte[] g_VerTmp = new byte[2048];
        
        const int MESSAGE_FP_RECEIVED = 0x0400 + 6;

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        public MainFrm()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {            
            if(!gConnected)
	        {
		        int ret = 0;
		        byte[] paramValue = new byte[64];
                
		        // Enable log
                Array.Clear(paramValue, 0, paramValue.Length);
                paramValue[0] = 1;
		        ZKFPCap.sensorSetParameterEx(g_Handle, 1100, paramValue, 4);

		        ret = ZKFPCap.sensorInit();
		        if(ret != 0)
		        {
                    MessageBox.Show("Init Failed, ret=" + ret.ToString());
			        return;
		        }
                g_Handle = ZKFPCap.sensorOpen(0);

                Array.Clear(paramValue, 0, paramValue.Length);
                ZKFPCap.sensorGetVersion(paramValue, paramValue.Length);

		        ret = paramValue.Length;
                Array.Clear(paramValue, 0, paramValue.Length);
                ZKFPCap.sensorGetParameterEx(g_Handle, 1, paramValue, ref ret);
		        g_nWidth = BitConverter.ToInt32(paramValue, 0);

                this.picFP.Width = g_nWidth;


                ret = paramValue.Length;
                Array.Clear(paramValue, 0, paramValue.Length);
                ZKFPCap.sensorGetParameterEx(g_Handle, 2, paramValue, ref ret);
                g_nHeight = BitConverter.ToInt32(paramValue, 0);

                this.picFP.Height = g_nHeight;

                ret = paramValue.Length;
                Array.Clear(paramValue, 0, paramValue.Length);
                ZKFPCap.sensorGetParameterEx(g_Handle, 106, paramValue, ref ret);
                g_FPBufferSize = BitConverter.ToInt32(paramValue, 0);

                g_FPBuffer = new byte[g_FPBufferSize];
                Array.Clear(g_FPBuffer, 0, g_FPBuffer.Length);

		        // get vid&pid
                ret = paramValue.Length;
                Array.Clear(paramValue, 0, paramValue.Length);
                ZKFPCap.sensorGetParameterEx(g_Handle, 1015, paramValue, ref ret);
                int nVid = BitConverter.ToInt16(paramValue, 0);
                int nPid = BitConverter.ToInt16(paramValue, 2);
        		
		        // Manufacturer
                ret = paramValue.Length;
                Array.Clear(paramValue, 0, paramValue.Length);
                ZKFPCap.sensorGetParameterEx(g_Handle, 1101, paramValue, ref ret);
                string manufacturer = System.Text.Encoding.ASCII.GetString(paramValue);
		        // Product
                ret = paramValue.Length;
                Array.Clear(paramValue, 0, paramValue.Length);
                ZKFPCap.sensorGetParameterEx(g_Handle, 1102, paramValue, ref ret);
                string product = System.Text.Encoding.ASCII.GetString(paramValue);
		        // SerialNumber
                ret = paramValue.Length;
                Array.Clear(paramValue, 0, paramValue.Length);
                ZKFPCap.sensorGetParameterEx(g_Handle, 1103, paramValue, ref ret);
                string serialNumber = System.Text.Encoding.ASCII.GetString(paramValue);
        				
                // Fingerprint Alg
                short[] iSize = new short[24];
	            iSize[0] = (short)g_nWidth;
                iSize[1] = (short)g_nHeight;
                iSize[20] = (short)g_nWidth;
                iSize[21] = (short)g_nHeight; ;
                g_biokeyHandle = ZKFinger10.BIOKEY_INIT(0, iSize, null, null, 0);
                if (g_biokeyHandle == IntPtr.Zero)
	            {
		            MessageBox.Show("BIOKEY_INIT failed");
                    return;
	            }

	            // Set allow 360 angle of Press Finger
                ZKFinger10.BIOKEY_SET_PARAMETER(g_biokeyHandle, 4, 180);

	            // Set Matching threshold
                ZKFinger10.BIOKEY_MATCHINGPARAM(g_biokeyHandle, 0, ZKFinger10.THRESHOLD_MIDDLE);

	            // Init RegTmps
                for (int i = 0; i < 3; i++)
                {
                    g_RegTmps[i] = new byte[2048];
                }

                Thread captureThread = new Thread(new ThreadStart(DoCapture));
                captureThread.IsBackground = true;
                captureThread.Start();
                g_bIsTimeToDie = false;

		        gConnected = true;
                btnRegister.Enabled = true;
                btnVerify.Enabled = true;
                btnConnect.Text = "Disconnect Sensor";
                
                txtPrompt.Text = "Please put your finger on the sensor";
                tssLbl1.Text = "请把手指放在传感器上。";
	        }
	        else
	        {
		        FreeSensor();

                ZKFinger10.BIOKEY_DB_CLEAR(g_biokeyHandle);
                ZKFinger10.BIOKEY_CLOSE(g_biokeyHandle);

                gConnected = false;
                btnRegister.Enabled = false;
                btnVerify.Enabled = false;
                btnConnect.Text = "Connect Sensor";
            }
        }

        private void FreeSensor()
        {
            g_bIsTimeToDie = true;
            Thread.Sleep(1000);
            ZKFPCap.sensorClose(g_Handle);

            // Disable log
            byte[] paramValue = new byte[4];
            paramValue[0] = 0;
            ZKFPCap.sensorSetParameterEx(g_Handle, 1100, paramValue, 4);

            ZKFPCap.sensorFree();
        }

        private void DoCapture()
        {
            while (!g_bIsTimeToDie)
            {
                int ret = ZKFPCap.sensorCapture(g_Handle, g_FPBuffer, g_FPBufferSize);
                if (ret > 0)
                {
                    SendMessage(g_FormHandle, MESSAGE_FP_RECEIVED, IntPtr.Zero, IntPtr.Zero);
                }
            }
        }

        protected override void DefWndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case MESSAGE_FP_RECEIVED:
                    {
                        try
                        {
                            MemoryStream ms = new MemoryStream();
                            BitmapFormat.GetBitmap(g_FPBuffer, g_nWidth, g_nHeight, ref ms);
                            Bitmap bmp = new Bitmap(ms);
                            this.picFP.Image = bmp;

                            txtStatus.Text = "IMAGE_READY";
                            tssLbl2.Text = "IMAGE_READY";

                            int ret = 0;
                            int id = 0;
                            int score = 0; 
                            int quality = 0;

                            if (g_IsRegister)
                            {
                                Array.Clear(g_RegTmp, 0, g_RegTmp.Length);
                                ret = ZKFinger10.BIOKEY_EXTRACT(g_biokeyHandle, g_FPBuffer, g_RegTmp, 0);
                                if (ret > 0)
                                {                                  
                                    Array.Copy(g_RegTmp, g_RegTmps[g_RegisterTimeCount++], ret);                       

                                    // Get fingerprint quality
                                    quality = ZKFinger10.BIOKEY_GETLASTQUALITY();
                                    txtQuality.Text = quality.ToString();
                                    tssLbl3.Text="图像质量："+ quality.ToString();

                                    txtPrompt.Text = string.Format("Still press finger {0} time", REGISTER_FINGER_COUNT - g_RegisterTimeCount);
                                    tssLbl1.Text = string.Format("Still press finger {0} time", REGISTER_FINGER_COUNT - g_RegisterTimeCount);
                                    if (g_RegisterTimeCount == REGISTER_FINGER_COUNT)
                                    {
                                        Array.Clear(g_RegTmp, 0, g_RegTmp.Length);

                                        int size = 0;

                                        /*unsafe
                                        {
                                            fixed (byte* Template1 = g_RegTmps[0])
                                            {
                                                fixed (byte* Template2 = g_RegTmps[1])
                                                {
                                                    fixed (byte* Template3 = g_RegTmps[2])
                                                    {
                                                        byte*[] pTemplate = new byte*[3] { Template1, Template2, Template3 };

                                                        size = ZKFinger10.BIOKEY_GENTEMPLATE(g_biokeyHandle, pTemplate, 3, g_RegTmp);
                                                    }
                                                }
                                            }
                                        }*/
                                        size = ZKFinger10.BIOKEY_GENTEMPLATE_SP(g_biokeyHandle, g_RegTmps[0], g_RegTmps[1], g_RegTmps[2], 3, g_RegTmp);

                                        if (size > 0)
                                        {

                                            try

                                            {
                                                ZKFinger10.BIOKEY_DB_ADD(g_biokeyHandle, ++g_RegisterCount, size, g_RegTmp);
                                                txtPrompt.Text = string.Format("Register succeeded, fid={0}, totalCount={1}", g_RegisterCount, ZKFinger10.BIOKEY_DB_COUNT(g_biokeyHandle));
                                                g_IsRegister = false;

                                                //判断是否存在数据
                                                if (g_IfExistID == true )
                                                {
                                                    //如果有数据，即true，只添加指纹
                                                    String insert = "insert into Users(FingerID1) values('" + g_RegTmp + "') where uid = '" + tbUserID.Text.Trim() + "'";
                                                    int count = SqlHelper.ExecuteNonQuery(SqlHelper.Conn, CommandType.Text, insert, null);
                                                    if (count != 0)
                                                    {
                                                        MessageBox.Show("插入指纹模板成功！");
                                                    }
                                                }
                                                else
                                                {
                                                    //没有数据，即false，新建用户并添加指纹
                                                    String insert = "insert into Users(uid,EmployeeName,FingerID1) values('" + tbUserID.Text.Trim() + "','" + tbName.Text.Trim() + "','" + g_RegTmp + "')";
                                                    int count = SqlHelper.ExecuteNonQuery(SqlHelper.Conn, CommandType.Text, insert, null);
                                                    if (count != 0)
                                                    {
                                                        MessageBox.Show("插入指纹模板成功！");
                                                    }
                                                }
                                                
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show(ex.Message);
                                            }

                                        }


                                        else
                                        {
                                            txtPrompt.Text = "Register failed";
                                            tssLbl1.Text = "Register failed";
                                        }
                                        g_RegisterTimeCount = 0;
                                    }
                                }
                                else
                                {
                                    txtPrompt.Text = "Extract template failed";
                                    tssLbl1.Text = "Extract template failed";
                                }
                            }
                            else
                            {
                                Array.Clear(g_VerTmp, 0, g_VerTmp.Length);   
                                if ((ret = ZKFinger10.BIOKEY_EXTRACT(g_biokeyHandle, g_FPBuffer, g_VerTmp, 0)) > 0)
                                {

                                    // Get fingerprint quality
                                    quality = ZKFinger10.BIOKEY_GETLASTQUALITY();
                                    txtQuality.Text = quality.ToString();
                                    tssLbl3.Text="图像质量："+ quality.ToString();

                                    ret = ZKFinger10.BIOKEY_IDENTIFYTEMP(g_biokeyHandle, g_VerTmp, ref id, ref score);
                                    if (ret > 0)
                                    {
                                        txtPrompt.Text = string.Format("认证成功, id={0}, 质量={1}", id, score);
                                        tssLbl1.Text= string.Format("认证成功, id={0}, 质量={1}", id, score);
                                    }
                                    else
                                    {
                                        txtPrompt.Text = string.Format("认证失败, 质量={0}", score);
                                        tssLbl1.Text = string.Format("认证失败, 质量={0}", score);
                                    }
                                }
                                else
                                {
                                    txtPrompt.Text = "Extract template failed";                                  
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message.ToString());
                        }
                    }
                    break;

                default:
                    base.DefWndProc(ref m); 
                    break;
            }
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            g_FormHandle = this.Handle;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            txtPrompt.Text = "注册指纹，请按手指3次";
            tssLbl1.Text= "注册指纹，请按手指3次";
            g_RegisterTimeCount = 0;           
            g_IsRegister = true;

            //判定是否已经存在该用户
            if (tbName.Text.Trim() == "" || tbUserID.Text.Trim() == "")
            {
                MessageBox.Show("请填写员工ID和姓名");
                return;
            }
            else
            {
                //开始连接数据库并添加到数据库
                //String a = "select * from Users where uid= '" + tbUserID.Text.Trim() + "'";
                //int count1 = SqlHelper.ExecuteNonQuery(SqlHelper.Conn, CommandType.Text, a, null);

                //指定Sql Server提供者的连接字符串
                string connString = "server=.;database =hsbs.WagesSystem;uid =sa;pwd=123456";
                //建立连接对象
                SqlConnection Sqlconn = new SqlConnection(connString);
                //打开连接
                Sqlconn.Open();
                //为上面的连接指定Command对象
                SqlCommand thiscommand = Sqlconn.CreateCommand();
                thiscommand.CommandText = "select * from Users where uid ="+ tbUserID.Text.Trim();
                //为指定的command对象执行DataReader
                SqlDataReader thisSqlDataReader = thiscommand.ExecuteReader();

                //只要有数据
                if (thisSqlDataReader.Read())
                {
                    //有数据返回真
                    g_IfExistID = true;
                    MessageBox.Show("该用户已存在");
                }
                else
                {
                    //没有数据返回假
                    g_IfExistID = false;
                    MessageBox.Show("新建用户");
                }
                //关闭读取
                thisSqlDataReader.Close();
                //关闭连接
                Sqlconn.Close();
                
                //if (count1 != -1)

                //{
                //    g_IfExistID = false;
                //    MessageBox.Show("false");
                //}
                //else
                //{
                //    g_IfExistID = true;
                //    MessageBox.Show("true");
                //}
                
               
            }//判定结束
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            txtPrompt.Text = "请把手指放在传感器上";
            tssLbl1.Text = "请把手指放在传感器上";
            g_IsRegister = false;
        }

    }
}
