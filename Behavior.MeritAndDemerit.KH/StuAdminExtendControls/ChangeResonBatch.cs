﻿using FISCA.LogAgent;
using FISCA.Presentation.Controls;
using JHSchool.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JHSchool.Behavior.MeritAndDemerit_KH
{
    public partial class ChangeResonBatch : BaseForm
    {
        public string ChangeText = "";
        List<JHDisciplineRecord> _helper;

        public ChangeResonBatch()
        {
            InitializeComponent();
        }
        public ChangeResonBatch(List<JHDisciplineRecord> helper)
        {
            InitializeComponent();

            _helper = helper;

            labelX3.Text = "事由範例:" + comboBoxEx2.Text.Trim() + _helper[0].Reason;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (comboBoxEx2.SelectedItem == null)
            {
                MsgBox.Show("未選擇前置詞,已取消操作!!");
                return;
            }

            if (string.IsNullOrEmpty(comboBoxEx2.SelectedItem.ToString()))
            {
                MsgBox.Show("未選擇前置詞,已取消操作!!");
                return;
            }
            DialogResult dr = MsgBox.Show("確認儲存增加所選之事由前置詞?", MessageBoxButtons.YesNo, MessageBoxDefaultButton.Button2);

            if (dr == System.Windows.Forms.DialogResult.Yes)
            {

                ChangeText = comboBoxEx2.SelectedItem.ToString();

                foreach (JHDisciplineRecord each in _helper)
                {
                    each.Reason = ChangeText + each.Reason;
                }

                try
                {
                    JHDiscipline.Update(_helper);

                    MsgBox.Show("資料儲存完成");
                    ApplicationLog.Log("獎懲批次修改", "修改", "批次增加事由前置詞「" + ChangeText + "」\n" + "共" + _helper.Count + "筆資料");
                    this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                }
                catch (Exception ex)
                {
                    MsgBox.Show("儲存發生錯誤:\n" + ex.Message);
                    SmartSchool.ErrorReporting.ReportingService.ReportException(ex);
                }
            }

        }


        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBoxEx2_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelX3.Text = "事由範例:" + comboBoxEx2.SelectedItem.ToString() + _helper[0].Reason;
        }
    }
}