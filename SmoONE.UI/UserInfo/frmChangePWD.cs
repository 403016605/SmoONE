using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.Application;
using System.Threading.Tasks;
using SmoONE.CommLib;
using SmoONE.Domain;

namespace SmoONE.UI
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.0
    // Copyright  (c)  2016-2017 Smobiler 
    // ����ʱ�䣺 2016/11
    // ��Ҫ���ݣ�  �����޸Ľ���
    // ******************************************************************
    partial class frmChangePWD : Smobiler.Core.MobileForm
    {
        #region "definition"
        public string oldPwd;//�޸�����
        bool isPwdC1 = false; //�������Ƿ���ʾ�����ַ�����
        bool isPwdC2 = false;//ȷ�������Ƿ���ʾ�����ַ�����
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
        /// <summary>
        /// �������Ƿ���ʾ�����ַ��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgbtnPwdC1_Click(object sender, EventArgs e)
        {
            if (isPwdC1 == false)
            {
                txtPwd1.PasswordChar = '*';//����textboxΪ�����ַ�
                imgbtnPwdC1.ResourceID = "!\\ue417043146223";
                imgbtnPwdC1.Refresh();
                isPwdC1 = true;
               
            }
            else
            {
                txtPwd1.PasswordChar = ' ';//textbox�����ַ�Ϊ��ʱ����ʾ����
                imgbtnPwdC1.ResourceID = "!\\ue8f5192192192";
                imgbtnPwdC1.Refresh();
                isPwdC1 = false;
            }
        }
        /// <summary>
        /// ȷ�������Ƿ���ʾ�����ַ��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void imgbtnPwdC2_Click(object sender, EventArgs e)
        {
            if (isPwdC2 == false)
            {
                txtPwd2.PasswordChar = '*';//����textboxΪ�����ַ�
                imgbtnPwdC2.ResourceID = "!\\ue417043146223";
                imgbtnPwdC2.Refresh();
                isPwdC2 = true;

            }
            else
            {
                txtPwd2.PasswordChar = ' ';//textbox�����ַ�Ϊ��ʱ����ʾ����
                imgbtnPwdC2.ResourceID = "!\\ue8f5192192192";
                imgbtnPwdC2.Refresh();
                isPwdC2 = false;
            }
        }
        /// <summary>
        /// �޸ı�������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string pwd1 = txtPwd1.Text.Trim();
                string pwd2 = txtPwd2.Text.Trim();
                if (pwd1.Length <= 0)
                {
                    throw new Exception("�����������룡");
                }
                if (pwd2.Length <= 0)
                {
                    throw new Exception("������ȷ�����룡");
                }
                if (!pwd1.Equals(pwd2))
                {
                    throw new Exception("�����������벻һ�£����飡");
                }
                if (pwd1.Length < 6 || pwd1.Length > 12)
                {
                    throw new Exception("���������Ϊ6-12λ��");
                }
                if (pwd2.Length < 6 || pwd2.Length > 12)
                {
                    throw new Exception("���������Ϊ6-12λ��");
                }
                if (oldPwd != null)
                {
                    //�����봦��,��������
                    string encryptPwd = AutofacConfig.userService.Encrypt(DateTime.Now.ToString("yyyyMMddHHmmss") + pwd2);
                    //��������
                    ReturnInfo result = AutofacConfig.userService.ChangePwd(Client .Session ["U_ID"].ToString (), oldPwd, encryptPwd);
                   //�������true���޸ĳɹ������򵯳�����
                    if (result.IsSuccess == true)
                    {
                        Close();
                        Toast("�����޸ĳɹ���", ToastLength.SHORT);
                    }
                    else
                    {
                        throw new Exception(result.ErrorInfo);
                    }
                }
            }
            catch(Exception ex)
            {
                Toast(ex.Message ,ToastLength.SHORT );
            }
        }
        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmChangePWD_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                Close();         //�رյ�ǰҳ��
            }
        }
        /// <summary>
        /// ������ͼƬ��ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmChangePWD_TitleImageClick(object sender, EventArgs e)
        {
            Close();
        } 

    }
}