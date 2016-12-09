using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.Application;
using SmoONE.DTOs;
using SmoONE.Domain;

namespace SmoONE.UI
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.0
    // Copyright  (c)  2016-2017 Smobiler 
    // ����ʱ�䣺 2016/11
    // ��Ҫ���ݣ� �����˻�����ѡ���б����
    // ******************************************************************
    partial class frmCheckOrCCTo : Smobiler.Core.MobileForm
    {
        #region "definition"
        string userName="";//�û�����
        public bool isCTemUser;//�Ƿ��ǳɱ�����ģ��������Աѡ��
        public bool isCCTO;//�Ƿ��ǳ�����Աѡ��
       public bool isCheck;//�Ƿ���������Աѡ��
       public string userInfo = "";//ѡ�е��û�
       AutofacConfig AutofacConfig = new AutofacConfig();//����������
      #endregion
       private void frmCheckOrCCTo_Load(object sender, EventArgs e)
        {
            GetDate();
        }
        /// <summary>
       /// gridUserData����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
       private void gridUserData_CellClick(object sender, GridViewCellEventArgs e)
        {
            //�����رյ�ǰ����
            userInfo = e.Cell.Items["lblUser"].Value + "," + e.Cell.Items["lblUser"].Text + "," + e.Cell.Items["imgPortrait"].Value;
            ShowResult = ShowResult.Yes;
            Close();
        }
        /// <summary>
        /// ������ѯ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1 .Text .Trim ().Length >0)
            {
                userName = textBox1.Text.Trim();
                GetDate();
            }
            
        }
        /// <summary>
        /// ��ȡ�����˻�������������
        /// </summary>
        private void GetDate()
        {
            List<UserDto> listUser =new List<UserDto>();//�����˻���������
             if (isCheck == true || isCTemUser == true)
            {
                //��ȡ���������ݻ�ɱ�����ģ������������
                listUser = AutofacConfig.userService.QueryCheckUsers(userName);
            }
            if (isCCTO  == true)
            {
                //��ȡ����������
                listUser = AutofacConfig.userService.QueryCCUsers(userName);
            }
            gridUserData.Rows.Clear();//��������˻������б�����

            //��������˻��������ݴ���0���������
            if (listUser.Count() > 0)
            {
                foreach (UserDto user in listUser)
                {
                    if (string.IsNullOrEmpty(user.U_Portrait) == true)
                    {
                        UserDetailDto userinfo = AutofacConfig.userService.GetUserByUserID(user.U_ID);
                        switch (userinfo.U_Sex)
                        {
                            case (int)Sex.��:
                                user.U_Portrait = "boy";
                                break;
                            case (int)Sex.Ů:
                                user.U_Portrait = "girl";
                                break;
                        }
                    }
                    else
                    {
                        user.U_Portrait = user.U_Portrait;
                    }
                }
                gridUserData.DataSource = listUser;
                gridUserData.DataBind();

            }
           
        }
        /// <summary>
        /// ������ͼƬ��ť����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCheckOrCCTo_TitleImageClick(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCheckOrCCTo_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                Close();         //�رյ�ǰҳ��
            }
        }
    }
}