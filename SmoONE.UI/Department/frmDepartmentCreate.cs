using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.CommLib;
using SmoONE.DTOs;
using SmoONE.Domain;

namespace SmoONE.UI.Department
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.0
    // Copyright  (c)  2016-2017 Smobiler
    // ����ʱ�䣺 2016/11
    // ��Ҫ���ݣ�  ���Ŵ�����༭����
    // ******************************************************************
    partial class frmDepartmentCreate : Smobiler.Core.MobileForm
    {
        #region "definition"
        public string D_ID;//���ű��
        string leader="";//������
        string D_Portrait="";//����ͷ��
        AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDep_Name.Text .Trim ().Length <= 0)
                {
                    throw new Exception("�����벿�����ƣ�");
                }
              
                if (leader.Length <= 0)
                {
                    throw new Exception("�����������ˣ�");
                }
                DepInputDto department = new DepInputDto();
                department.Dep_Name = txtDep_Name.Text;
                if (txtProDay.Text.Trim().Length > 0)
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(txtProDay.Text.Trim(), @"^(?!0+(?:\.0+)?$)(?:[1-9]\d*|0)(?:\.\d{1,2})?$") == false)
                    {
                        throw new Exception("��Ŀ�������Ϊ����0�����֣�");
                    }

                    else
                    {
                        department.Dep_ProDay = Convert.ToDecimal(txtProDay.Text);
                    }
                }
                else
                {
                    department.Dep_ProDay = 0;
                }
                if (txtOtherDay.Text.Trim().Length > 0)
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(txtProDay.Text.Trim(), @"^(?!0+(?:\.0+)?$)(?:[1-9]\d*|0)(?:\.\d{1,2})?$") == false)
                    {
                        throw new Exception("��Ŀ�����������Ϊ����0�����֣�");
                    }
                    else
                    {
                        department.Dep_OtherDay = Convert.ToDecimal(txtOtherDay.Text);
                    }
                }
                else
                {
                    department.Dep_OtherDay = 0;
                }
                department.Dep_UpdateUser = Client.Session["U_ID"].ToString();
                department.Dep_Leader = leader;
                if (string.IsNullOrEmpty(D_Portrait) == false)
                {
                    department.Dep_Icon = D_Portrait;
                }
                else
                {
                    department.Dep_Icon = "";
                }
                if (string.IsNullOrEmpty(D_ID) == false)
                {
                    department.Dep_ID = D_ID;
                    List<UserDto> listuserDto=  AutofacConfig .userService  .GetUserByDepID(D_ID);
                    List <string > listUser=new List<string> ();
                    foreach (UserDto user in listuserDto)
                    {
                        listUser.Add (user.U_ID);
                    }
                    department.UserIDs=listUser;
                    ReturnInfo result = AutofacConfig.departmentService.UpdateDepartment(department);
                    if (result.IsSuccess == false)
                    {
                        throw new Exception(result.ErrorInfo);
                    }
                    else
                    {
                        ShowResult = ShowResult.Yes;
                        Toast("���ű���ɹ���", ToastLength.SHORT);
                    }
                }
                else
                {
                    frmDepAssignUser frmDepAssignUser = new frmDepAssignUser();
                    frmDepAssignUser.department = department;
                    Redirect(frmDepAssignUser);
                }
                
            }
            catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
        }
        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLeader_Click(object sender, EventArgs e)
        {
            popLeader.Groups.Clear();
            PopListGroup poli = new PopListGroup();
            popLeader.Groups.Add(poli);
            poli.Text = "������ѡ��";
            List<UserDto> listuser = AutofacConfig.userService.GetAllUsers();
            foreach (UserDto user in listuser)
            {
                poli.Items.Add(user.U_Name, user.U_ID);
                if (leader.Trim().Length > 0)
                {
                    if (leader.Trim().Equals(user.U_ID))
                    {
                        popLeader.SetSelections(poli.Items[(poli.Items.Count - 1)]);
                    }
                }
            }
            popLeader.Show();
        }

        private void frmDepartmentCreate_Load(object sender, EventArgs e)
        {
            Bind();
        }

        /// <summary>
        /// ��ʼ���¼�
        /// </summary>
        private void Bind()
        {
            try 
            {
                if (D_ID != null)
                {
                    DepDetailDto dep = AutofacConfig.departmentService.GetDepartmentByDepID(D_ID);
                    if (dep == null)
                    {
                        throw new Exception("����" + D_ID + "�����ڣ����飡");
                    }
                    txtDep_Name.Text = dep.Dep_Name;
                    txtProDay.Text = dep.Dep_ProDay.ToString();
                    txtOtherDay.Text = dep.Dep_OtherDay.ToString();
                    leader = dep.Dep_Leader;
                    if (string.IsNullOrEmpty(dep.Dep_Icon) == false)
                    {
                        D_Portrait = dep.Dep_Icon;
                        imgPortrait.ResourceID = dep.Dep_Icon;
                    }
                    else
                    {
                        imgPortrait.ResourceID = "bumenicon";
                    }
                    UserDetailDto userinfo = AutofacConfig.userService.GetUserByUserID(leader);
                    btnLeader.Text = userinfo.U_Name;
                    btnSave.Text = "�ύ";
                    btnAssignUser.Visible = true;
                    btnSave.Top = 270;
                }
                else
                {
                    btnAssignUser.Visible =false ;
                    btnSave.Top = 225;
                }
            }
                catch (Exception ex)
            {
                    MessageBox .Show (ex.Message );
             }
        }
        /// <summary>
        /// �����˸�ֵ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popLeader_Selected(object sender, EventArgs e)
        {
            if (popLeader.Selection != null)
            {
                if (AutofacConfig.departmentService.IsLeader(popLeader.Selection.Value) == false)
                {
                    leader = popLeader.Selection.Value;
                    btnLeader.Text = popLeader.Selection.Text;
                }
                else
                {
                    Toast(popLeader.Selection.Text+"���ǲ��������ˣ����Ƚ�ɢ���ţ�");
                }
            }
        }
        /// <summary>
        /// �ֻ��Դ����˰�ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDepartmentCreate_KeyDown(object sender, KeyDownEventArgs e)
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
        private void frmDepartmentCreate_TitleImageClick(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// �ϴ�����ͷ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Click(object sender, EventArgs e)
        {
            cameraPortrait.GetPhoto();
        }
        /// <summary>
        /// ���沢��ֵͷ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cameraPortrait_ImageCaptured(object sender, BinaryData e)
        {
            if (string.IsNullOrEmpty(e.ErrorInfo))
            {

                if (imgPortrait.ResourceID.Trim().Length > 0 & string.IsNullOrEmpty(D_Portrait)==false )
                {
                    e.SaveFile(D_Portrait+".png");
                    imgPortrait.ResourceID = D_Portrait;
                    imgPortrait.Refresh();
                }
                else
                {
                    e.SaveFile(e.ResourceID + ".png");
                    D_Portrait = e.ResourceID;
                    imgPortrait.ResourceID = e.ResourceID;
                    imgPortrait.Refresh();
                }
            }
        }
        /// <summary>
        /// ��ת�����䲿�Ž���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAssignUser_Click(object sender, EventArgs e)
        {
            if (D_ID != null)
            {
                DepDetailDto dep = AutofacConfig.departmentService.GetDepartmentByDepID(D_ID);
                if (dep != null)
                {
                    DepInputDto department = new DepInputDto();
                    department.Dep_ID = dep.Dep_ID;
                    department.Dep_Name = dep.Dep_Name;
                    department.Dep_Leader = dep.Dep_Leader;
                    department.Dep_Icon = dep.Dep_Icon;
                    frmDepAssignUser frmDepAssignUser = new frmDepAssignUser();
                    frmDepAssignUser.department = department;
                    Redirect(frmDepAssignUser);
                }

            }
        }
    }
}