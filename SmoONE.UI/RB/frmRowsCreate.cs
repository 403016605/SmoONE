﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smobiler.Core;
using Smobiler.Core.Controls;
using SmoONE.CommLib;
using SmoONE.DTOs;


namespace SmoONE.UI.RB
{
    // ******************************************************************
    // 文件版本： SmoONE 1.0
    // Copyright  (c)  2016-2017 Smobiler 
    // 创建时间： 2016/11
    // 主要内容：  消费记录创建或编辑界面
    // ******************************************************************
    partial class frmRowsCreate : Smobiler.Core.MobileForm
    {

        #region "Properties"
        internal string ID;               //消费记录编号
        private string TYPEID;            //消费类型编号
        AutofacConfig AutofacConfig = new AutofacConfig();//调用配置类
        #endregion

        /// <summary>
        /// 消费类型选择按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void btnRBType_Click(object sender, EventArgs e)
        {
            try
            {
                //进入消费类型列表
                frmRTypeChoose frm = new frmRTypeChoose();
                this.Redirect(frm, (MobileForm sender1, object args) =>
                {
                    try
                    {
                        if (frm.ShowResult == Smobiler.Core.ShowResult.Yes)
                        {
                            string TYPEIDs = frm.TYPEID;
                            if (TYPEIDs.Length > 0)
                            {
                                string[] types = TYPEIDs.Split(new char[] { '/' });
                                TYPEID = types[0];       //消费类型编号
                                this.btnRBType.Text = types[1];               //消费类型名称
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                });
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// 消费模板按钮选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void btnRBModel_Click(object sender, EventArgs e)
        {
            try
            {
                //进入消费模板列表
                frmRTypeTempChoose frm = new frmRTypeTempChoose();
                this.Redirect(frm, (MobileForm sender1, object args) =>
                {
                    if (frm.ShowResult == Smobiler.Core.ShowResult.Yes)
                    {
                        //成功选择消费模板后，给页面自动赋值
                        string TemplateID = frm.RTTemplaetID;
                        RB_RType_TemplateDto RBTemp = AutofacConfig.rBService.GetTemplateByTemplateID(TemplateID);
                        string RBTypeName = AutofacConfig.rBService.GetTypeNameByID(RBTemp.RB_RTT_TypeID);
                        this.btnRBModel.Text = "已选择";
                        TYPEID = RBTemp.RB_RTT_TypeID;                //报销类型ID
                        this.txtMoney.Text = RBTemp.RB_RTT_Amount.ToString();          //消费金额
                        this.btnRBType.Text = RBTypeName;       //报销类型名称
                        this.txtNote.Text = RBTemp.RB_RTT_Note;          //消费备注
                    }
                });
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// 初始化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks></remarks>
        private void frmConsumption_Load(object sender, EventArgs e)
        {
            try
            {
                Bind();           //初始化数据
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// 初始化方法
        /// </summary>
        /// <remarks></remarks>
        private void Bind()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ID) == false)
                {         
                    int RID = Convert.ToInt32(ID);       //将ID转换成Int类型
                    RB_RowsDto row = AutofacConfig.rBService.GetRowByRowID(RID);
                    string TypeName = AutofacConfig.rBService.GetTypeNameByID(row.R_TypeID);
                    this.TitleText = "消费记录";
                    TYPEID = row.R_TypeID;           //消费类型编号
                    this.btnRBType.Text = TypeName;           //消费类型名称
                    this.txtMoney.Text = row.R_Amount.ToString();       //消费金额
                    this.txtNote.Text = row.R_Note;                 //消费备注
                }
                else
                {
                    this.TitleText = "消费记录创建";
                    this.btnRBType.Text = "选择类型";
                    this.btnDelete.Visible = false;
                    btnSave.Width = 280;
                    btnSave.Left = 10;
                 
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// 手机自带返回键操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmConsumption_KeyDown(object sender, KeyDownEventArgs e)
        {
            if (e.KeyCode == KeyCode.Back)
            {
                this.Close();         //关闭当前页面
            }
        }
       
        /// <summary>
        /// 左上角按钮操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmConsumption_TitleImageClick(object sender, EventArgs e)
        {
            this.Close();         //关闭当前页面
        }
        /// <summary>
        /// 创建消费记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.txtMoney.Text))
                {
                    throw new Exception("请输入消费金额！");
                }
               else 
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(txtMoney.Text.Trim(), @"^(?!0+(?:\.0+)?$)(?:[1-9]\d*|0)(?:\.\d{1,2})?$") == false)
                    {
                        throw new Exception("金额必须为大于0的数字！");
                    }
                }
                if (string.IsNullOrEmpty(TYPEID))
                {
                    throw new Exception("请选择消费类别！");
                }
                if (string.IsNullOrEmpty(this.txtNote.Text))
                {
                    throw new Exception("请输入备注！");
                }
                RB_RowsInputDto Row = new RB_RowsInputDto();
                Row.R_Amount = decimal.Parse(this.txtMoney.Text);            //消费金额
                Row.R_Note = this.txtNote.Text;                    //明细
                Row.R_TypeID = TYPEID;           //消费类型
                Row.R_ConsumeDate = this.DatePicker.CurrentDate;  //消费记录日期
                if (string.IsNullOrWhiteSpace(ID) == false)
                {
                    int RID = Convert.ToInt32(ID);       //将ID转换成Int类型
                    Row.R_ID = Convert.ToInt32(ID);
                    ReturnInfo r = AutofacConfig.rBService.UpdateRB_Rows(Row);
                    if (r.IsSuccess == true)
                    {
                        this.ShowResult = Smobiler.Core.ShowResult.Yes;
                        this.Close();                       
                        Toast("消费记录修改成功");
                    }
                    else
                    {
                        Toast(r.ErrorInfo);
                    }
                }
                else
                {                     
                        Row.R_CreateUser = Client.Session["U_ID"].ToString();                      //创建用户                     
                        ReturnInfo r = AutofacConfig.rBService.CreateRB_Rows(Row);           //数据库创建消费记录
                        if (r.IsSuccess == true)
                        {
                            this.ShowResult = Smobiler.Core.ShowResult.Yes;
                            this.Close();
                            Toast("消费记录提交成功！");
                        }
                        else
                        {
                            Toast(r.ErrorInfo);
                        }                                
                }
            }
            catch (Exception ex)
            {
                Toast(ex.Message);
            }
        }
        /// <summary>
        /// 执行删除消费记录操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int RID = Convert.ToInt32(ID);
            ReturnInfo r=AutofacConfig.rBService.DeleteRB_Rows(RID);
            if (r.IsSuccess == true)
            {
                this.ShowResult = Smobiler.Core.ShowResult.Yes;
                this.Close();
                Toast("您已成功删除消费记录");
            }
            else
            {
                Toast(r.ErrorInfo);
            }
        }  
    }
}