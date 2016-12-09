using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.DTOs;
using SmoONE.CommLib;
using SmoONE.Domain;

namespace SmoONE.UI.Department
{
    // ******************************************************************
    // �ļ��汾�� SmoONE 1.0
    // Copyright  (c)  2016-2017 Smobiler 
    // ����ʱ�䣺 2016/11
    // ��Ҫ���ݣ�  ������Ա�������
    // ******************************************************************
    partial class frmDepAssignUser : Smobiler.Core.MobileForm
    {
        #region "definition"
        int selectUserQty = 0;//ѡ����Ա��
         public DepInputDto department;//������Ϣ
         AutofacConfig AutofacConfig = new AutofacConfig();//����������
        #endregion
      
        /// <summary>
        /// �ֻ��Դ����˼���ť�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDepAssignUser_KeyDown(object sender, KeyDownEventArgs e)
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
        private void frmDepAssignUser_TitleImageClick(object sender, EventArgs e)
        {
            Close();
        }
        /// <summary>
        /// ��ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDepAssignUser_Load(object sender, EventArgs e)
        {
            Bind();
           
        }
        /// <summary>
        /// ��ʼ������
        /// </summary>
        private void Bind()
        {
            try
            {
                if (department != null)
                {
                    lblDepName.Text = department.Dep_Name;
                    lblLeader.Text = AutofacConfig.userService.GetUserByUserID(department.Dep_Leader).U_Name;
                    List<DataGridviewDepbyUser> listUser = new List<DataGridviewDepbyUser>();
                    List<UserDto> listDepUser = AutofacConfig.userService.GetAllUsers();//��ȡ���䲿����Ա
                    //���Ŵ���ʱgridview������
                    if (string.IsNullOrEmpty(department.Dep_ID) == true )
                    {
                        if (listDepUser.Count > 0)
                        { 
                            //��δ���䲿���Ҳ��ǵ�ǰ���������˵��û�����ӵ�listUser��
                            foreach (UserDto user in listDepUser)
                            {
                                if ((string.IsNullOrEmpty(user.U_DepID) == true) & (!department.Dep_Leader .Equals(user.U_ID)))
                                {
                                    DataGridviewDepbyUser depUser = new DataGridviewDepbyUser();
                                    depUser.U_ID = user.U_ID;
                                    depUser.U_Name = user.U_Name;
                                    if (string.IsNullOrEmpty(user.U_Portrait) == true)
                                    {
                                        switch (user.U_Sex)
                                        {
                                            case (int)Sex.��:
                                                depUser.U_Portrait = "boy";
                                                break;
                                            case (int)Sex.Ů:
                                                depUser.U_Portrait = "girl";
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        depUser.U_Portrait = user.U_Portrait;
                                    }
                                    depUser.U_DepID = "";
                                    depUser.U_DepName = "";
                                    depUser.SelectCheck = false;
                                    listUser.Add(depUser);
                                }
                            }
                            //���ѷ��䲿���Ҳ��ǵ�ǰ���������˵��û�����ӵ�listUser��
                            foreach (UserDto user in listDepUser)
                            {
                                if ((string.IsNullOrEmpty(user.U_DepID) == false) & (!department.Dep_Leader.Equals(user.U_ID)))
                                {
                                    DataGridviewDepbyUser depUser = new DataGridviewDepbyUser();
                                    depUser.U_ID = user.U_ID;
                                    depUser.U_Name = user.U_Name;
                                    if (string.IsNullOrEmpty(user.U_Portrait) == true)
                                    {
                                        switch (user.U_Sex)
                                        {
                                            case (int)Sex.��:
                                                depUser.U_Portrait = "boy";
                                                break;
                                            case (int)Sex.Ů:
                                                depUser.U_Portrait = "girl";
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        depUser.U_Portrait = user.U_Portrait;
                                    }
                                    depUser.U_DepID = user.U_DepID;
                                    string DepName = AutofacConfig.departmentService.GetDepartmentByDepID(user.U_DepID).Dep_Name;
                                    depUser.U_DepName = DepName;
                                    depUser.SelectCheck = false;
                                    listUser.Add(depUser);
                                }
                            }
                        }
                    }
                    //���ű༭ʱgridview������
                    if (string.IsNullOrEmpty(department.Dep_ID) == false )
                    {
                        if (listDepUser.Count > 0)
                        {
                            //����ǰ�����Ҳ��ǵ�ǰ���������˵��û�����ӵ�listUser��
                            foreach (UserDto user in listDepUser)
                            {
                                if ((string.IsNullOrEmpty(user.U_DepID) == false) & (department.Dep_ID.Equals(user.U_DepID)) & (!department.Dep_Leader.Equals(user.U_ID)))
                                {
                                    DataGridviewDepbyUser depUser = new DataGridviewDepbyUser();
                                    depUser.U_ID = user.U_ID;
                                    depUser.U_Name = user.U_Name;
                                    if (string.IsNullOrEmpty(user.U_Portrait) == true)
                                    {
                                        switch (user.U_Sex)
                                        {
                                            case (int)Sex.��:
                                                depUser.U_Portrait = "boy";
                                                break;
                                            case (int)Sex.Ů:
                                                depUser.U_Portrait = "girl";
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        depUser.U_Portrait = user.U_Portrait;
                                    }
                                    depUser.U_DepID = department .Dep_ID ;
                                    depUser.U_DepName =department .Dep_Name ;
                                    depUser.SelectCheck = true ;
                                    listUser.Add(depUser);
                                }
                            }
                            //��δ���䲿���Ҳ��ǵ�ǰ���������˵��û�����ӵ�listUser��
                            foreach (UserDto user in listDepUser)
                            {
                                if ((string.IsNullOrEmpty(user.U_DepID) == true) & (!department.Dep_Leader.Equals(user.U_ID)))
                                {
                                    DataGridviewDepbyUser depUser = new DataGridviewDepbyUser();
                                    depUser.U_ID = user.U_ID;
                                    depUser.U_Name = user.U_Name;
                                    if (string.IsNullOrEmpty(user.U_Portrait) == true)
                                    {
                                        switch (user.U_Sex)
                                        {
                                            case (int)Sex.��:
                                                depUser.U_Portrait = "boy";
                                                break;
                                            case (int)Sex.Ů:
                                                depUser.U_Portrait = "girl";
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        depUser.U_Portrait = user.U_Portrait;
                                    }
                                    depUser.U_DepID = "";
                                    depUser.U_DepName = "";
                                    depUser.SelectCheck = false;
                                    listUser.Add(depUser);
                                }
                            }
                            //���ѷ��䲿���Ҳ��ǵ�ǰ���ŵ��û�����ӵ�listUser��
                            foreach (UserDto user in listDepUser)
                            {
                                if ((string.IsNullOrEmpty(user.U_DepID) == false) & (!department.Dep_ID.Equals(user.U_DepID)) & (!department.Dep_Leader.Equals(user.U_ID)))
                                {
                                    DataGridviewDepbyUser depUser = new DataGridviewDepbyUser();
                                    depUser.U_ID = user.U_ID;
                                    depUser.U_Name = user.U_Name;
                                    if (string.IsNullOrEmpty(user.U_Portrait) == true)
                                    {
                                        switch (user.U_Sex)
                                        {
                                            case (int)Sex.��:
                                                depUser.U_Portrait = "boy";
                                                break;
                                            case (int)Sex.Ů:
                                                depUser.U_Portrait = "girl";
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        depUser.U_Portrait = user.U_Portrait;
                                    }
                                    depUser.U_DepID = user.U_DepID;
                                    string DepName = AutofacConfig.departmentService.GetDepartmentByDepID(user.U_DepID).Dep_Name;
                                    depUser.U_DepName = DepName;
                                    depUser.SelectCheck = false;
                                    listUser.Add(depUser);
                                }
                            }
                         }
                    }
                   
                    gridUserData.Rows.Clear();//�����Ա�б�����
                    if (listUser.Count > 0)
                    {
                        gridUserData.DataSource = listUser; //��gridView����
                        gridUserData.DataBind();

                    }
                   
                }
                else
                {
                    throw new Exception("�����벿����Ϣ��");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
      
        /// <summary>
        /// ȫѡ
        /// </summary>
        private void Checkall()
        {
            switch (checkAll .Checked )
            {
                case true:
                    foreach (GridViewRow rows in gridUserData.Rows)
                    {
                        rows.Cell.Items["Check"].DefaultValue = true;
                    }
                    selectUserQty = gridUserData.Rows.Count;
                    break;
                case false:
                    foreach (GridViewRow rows in gridUserData.Rows)
                    {
                        rows.Cell.Items["Check"].DefaultValue = false;
                    }
                    selectUserQty = 0;
                    break;
            }
        }
        /// <summary>
        /// ���䲿����Ա
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> listUser = new List<string>(); //�û�����
                string assignUser = "";//�ѷ��䲿���û�
                string depLeader = "";//�����������û�
                listUser.Add(department.Dep_Leader);//��Ӳ��Ÿ�����
                //��ȡ�����˵Ĳ���
                UserDetailDto leader = AutofacConfig.userService.GetUserByUserID(department.Dep_Leader);
               //������ű�Ų�Ϊ���Ҳ����ڵ�ǰ����ʱ������������ӵ��ѷ��䲿����Ա��
                if (string.IsNullOrEmpty(leader.U_DepID) == false)
                {   
                    if (string.IsNullOrEmpty(department.Dep_ID) == false)
                    {
                        if (!department.Dep_ID.Equals(leader.U_DepID))
                        {
                            assignUser = lblLeader.Text.Trim();
                        }
                    }
                }
                foreach (GridViewRow rows in gridUserData.Rows)
                {

                    if ((Convert.ToBoolean(rows.Cell.Items["Check"].DefaultValue) == true) &(!department.Dep_Leader.Equals(rows.Cell.Items["lblUser"].Value.ToString())))
                    {
                        string user = rows.Cell.Items["lblUser"].Value.ToString();
                        listUser.Add(user);
                        //��ȡ�ѷ����Ҳ����ڵ�ǰ���ŵ��û�
                        if (string.IsNullOrEmpty(rows.Cell.Items["lblDep"].Value .ToString ())==false )
                        {
                            if (string.IsNullOrEmpty(department.Dep_ID) == false)
                            {
                                if (!department.Dep_ID.Equals(rows.Cell.Items["lblDep"].Value.ToString()))
                                {
                                    //����ǲ��������ˣ�����ӵ������������û�depLeader�У�������ӵ��ѷ��䲿���û�assignUser��
                                    if (AutofacConfig.departmentService.IsLeader(rows.Cell.Items["lblUser"].Value.ToString()) == true)
                                    {
                                        if (string.IsNullOrEmpty(depLeader) == true)
                                        {
                                            depLeader = rows.Cell.Items["lblUser"].Text;
                                        }
                                        else
                                        {
                                            depLeader += "," + rows.Cell.Items["lblUser"].Text;
                                        }

                                    }
                                    else
                                    {
                                        if (string.IsNullOrEmpty(assignUser) == true)
                                        {
                                            assignUser = rows.Cell.Items["lblUser"].Text;
                                        }
                                        else
                                        {
                                            assignUser += "," + rows.Cell.Items["lblUser"].Text;
                                        }
                                    }
                                   
                                }
                            }
                        }
                    }
                }
                if (string.IsNullOrEmpty(depLeader) == false)
                {
                    throw new Exception(depLeader+"���ǲ��������ˣ����Ƚ�ɢ���ţ�");
                }
                bool isUPdateDep = false; //�Ƿ���²�����Ա
                if (string.IsNullOrEmpty(assignUser) == false)
                {
                    MessageBox.Show(assignUser+"�ѷ��䲿���Ƿ���²��ţ�", "������Ա", MessageBoxButtons.YesNo, (Object s, MessageBoxHandlerArgs args) =>
                    {
                        if (args.Result == Smobiler.Core.ShowResult.Yes)
                        {
                            isUPdateDep = true;
                        }
                    }
                      );
                }
                else
                {
                    isUPdateDep = true;
                }
                ReturnInfo result ;
                if (isUPdateDep == true)
                {
                    department.UserIDs = listUser;
                    if (department.Dep_ID != null)
                    {
                        result = AutofacConfig.departmentService.UpdateDepartment(department);
                    }
                    else
                    {
                        result = AutofacConfig.departmentService.AddDepartment(department);
                    }
                    if (result.IsSuccess == false)
                    {
                        throw new Exception(result.ErrorInfo);
                    }
                    else
                    {

                        Toast("������Ա����ɹ���", ToastLength.SHORT);
                    }
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message, ToastLength.SHORT);
            }
        }
        /// <summary>
        /// ȫѡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAll_Click(object sender, EventArgs e)
        {
            Checkall();
        }
        /// <summary>
        /// ȫѡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkAll_CheckChanged(object sender, CheckEventArgs e)
        {
            Checkall();
        }
        /// <summary>
        /// gridview����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_ItemClick(object sender, GridViewCellItemEventArgs e)
        {
            upCheckState();
        }
        /// <summary>
        /// ����ȫѡ״̬
        /// </summary>
        private void upCheckState()
        {
             selectUserQty = 0;
            foreach (GridViewRow rows in gridUserData.Rows)
            {

                if (Convert.ToBoolean(rows.Cell.Items["Check"].DefaultValue) == true)
                {
                    selectUserQty += 1;
                }
            }
            //��gridView����ѡ����������gridView����ʱ��Ϊȫѡ״̬������Ϊ��ѡ״̬��
            if (selectUserQty == gridUserData.Rows.Count)
            {
                checkAll.Checked = true;
            }
            else
            {
                checkAll.Checked = false;
            }
        }
        /// <summary>
        /// gridview����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CellClick(object sender, GridViewCellEventArgs e)
        {
            upCheckState();
        }
    }
}