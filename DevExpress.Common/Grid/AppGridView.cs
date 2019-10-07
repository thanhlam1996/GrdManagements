using System;
using DevExpress.XtraGrid.Views.Grid;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System.Drawing;
using DevExpress.XtraGrid.Columns;
using System.Data;
using DevExpress.XtraGrid.Views.Base;
using System.Collections;
using System.Collections.Generic;

namespace DevExpress.Common.Grid
{
    public static class AppGridView
    {
        /// <summary>
        /// Init luoi
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="autoFilter"></param>
        /// <param name="multiSelect"></param>
        /// <param name="selectMode"></param>
        /// <param name="detailButton"></param>
        /// <param name="groupPanel"></param>
        public static void InitGridView(GridView grid, bool autoFilter, bool multiSelect, GridMultiSelectMode selectMode, bool detailButton, bool groupPanel)
        {
            //Show filter
            grid.OptionsView.ShowAutoFilterRow = autoFilter;
            //Show multi select
            grid.OptionsSelection.MultiSelect = multiSelect;
            //Show multi select mode
            grid.OptionsSelection.MultiSelectMode = selectMode;
            //Show detail button
            grid.OptionsView.ShowDetailButtons = detailButton;
            //Show group panel
            grid.OptionsView.ShowGroupPanel = groupPanel;

            for (int i = 0; i < grid.Columns.Count; i++)
                grid.Columns[i].Visible = false;
        }

        /// <summary>
        /// Cai dat luoi
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="autoFilter"></param>
        /// <param name="multiSelect"></param>
        /// <param name="selectMode"></param>
        /// <param name="detailButton"></param>
        /// <param name="groupPanel"></param>
        /// <param name="textNewRow"></param>
        public static void InitGridView(GridView grid, bool autoFilter, bool multiSelect, GridMultiSelectMode selectMode, bool detailButton, bool groupPanel, string textNewRow)
        {
            //Show filter
            grid.OptionsView.ShowAutoFilterRow = autoFilter;
            //Show multi select
            grid.OptionsSelection.MultiSelect = multiSelect;
            //Show multi select mode
            grid.OptionsSelection.MultiSelectMode = selectMode;
            //Show detail button
            grid.OptionsView.ShowDetailButtons = detailButton;
            //Show group panel
            grid.OptionsView.ShowGroupPanel = groupPanel;
            //Show text new row
            grid.NewItemRowText = textNewRow;

            for (int i = 0; i < grid.Columns.Count; i++)
                grid.Columns[i].Visible = false;
        }

        /// <summary>
        /// Cai dat luoi
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="drGridOption"></param>
        /// <param name="dtColumnOption"></param>
        /// <param name="foreignLan"></param>
        public static void InitGridView(GridView grid, DataRow drGridOption, DataTable dtColumnOption, bool foreignLan)
        {
            try
            {
                //ShowAutoFilterRow
                if (drGridOption["ShowAutoFilterRow"].ToString().ToUpper() == "TRUE")
                    grid.OptionsView.ShowAutoFilterRow = true;
                else
                    grid.OptionsView.ShowAutoFilterRow = false;

                //MultiSelect
                if (drGridOption["MultiSelect"].ToString().ToUpper() == "TRUE")
                    grid.OptionsSelection.MultiSelect = true;
                else
                    grid.OptionsSelection.MultiSelect = false;

                //MultiSelectMode
                switch (drGridOption["MultiSelectMode"].ToString().ToUpper())
                {
                    case "CELL":
                        grid.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
                        break;
                    case "ROW":
                        grid.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
                        break;
                    default:
                        grid.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
                        break;
                }                

                //ShowGroupPanel
                if (drGridOption["ShowGroupPanel"].ToString().ToUpper() == "TRUE")
                    grid.OptionsView.ShowGroupPanel = true;
                else
                    grid.OptionsView.ShowGroupPanel = false;

                //GroupPanelText
                if (foreignLan)
                    grid.GroupPanelText = "Drap a column header here to group by that column";
                else
                    grid.GroupPanelText = "Thả tiêu đề cột muốn nhóm vào đây";

                //NewItemRowPosition
                switch (drGridOption["NewItemRowPosition"].ToString().ToUpper())
                {
                    case "TOP":
                        grid.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
                        break;
                    case "BOTTOM":
                        grid.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
                        break;
                    default:
                        grid.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                        break;
                }

                //NewItemRowText
                if (foreignLan)
                    grid.NewItemRowText = "Click here to add a new row";
                else
                    grid.NewItemRowText = "Nhấn vào đây để thêm mới";

                //ShowViewCaption
                if (drGridOption["ShowViewCaption"].ToString().ToUpper() == "TRUE")
                    grid.OptionsView.ShowViewCaption = true;
                else
                    grid.OptionsView.ShowViewCaption = false;

                //ViewCaption
                if (foreignLan)
                    grid.ViewCaption = drGridOption["ForeignGridName"].ToString();
                else
                    grid.ViewCaption = drGridOption["GridName"].ToString();

                for (int i = 0; i < grid.Columns.Count; i++)
                    grid.Columns[i].VisibleIndex = -1;

                DataView dv = dtColumnOption.DefaultView;
                dv.Sort = "VisibleIndex DESC";
                DataTable dtColumnVisible = dv.ToTable();

                DataRow[] drVisible = dtColumnVisible.Select("Visible = '" + true + "'");
                DataRow dr;
                bool showFooter = false;

                for (int i = 0; i < drVisible.Length; i++)
                {
                    dr = drVisible[i];

                    if (foreignLan)
                        grid.Columns[dr["ID"].ToString()].Caption = dr["ForeignColumnName"].ToString();
                    else
                        grid.Columns[dr["ID"].ToString()].Caption = dr["ColumnName"].ToString();

                    grid.Columns[dr["ID"].ToString()].Visible = true;

                    if (dr["ReadOnly"].ToString().ToUpper() == "TRUE")
                        grid.Columns[dr["ID"].ToString()].OptionsColumn.ReadOnly = true;
                    else
                        grid.Columns[dr["ID"].ToString()].OptionsColumn.ReadOnly = false;

                    switch (dr["SummaryType"].ToString().ToUpper())
                    {
                        case "COUNT":
                            SummaryField(grid, dr["ID"].ToString(), "Số lượng= {0:#,0}", DevExpress.Data.SummaryItemType.Count);
                            showFooter = true;
                            break;
                        case "SUM":
                            SummaryField(grid, dr["ID"].ToString(), "Tổng= {0:#,0}", DevExpress.Data.SummaryItemType.Sum);
                            showFooter = true;
                            break;
                        case "AVERAGE":
                            SummaryField(grid, dr["ID"].ToString(), "Trung bình= {0:#,0}", DevExpress.Data.SummaryItemType.Average);
                            showFooter = true;
                            break;
                        case "MIN":
                            SummaryField(grid, dr["ID"].ToString(), "Nhỏ nhất= {0:#,0}", DevExpress.Data.SummaryItemType.Min);
                            showFooter = true;
                            break;
                        case "MAX":
                            SummaryField(grid, dr["ID"].ToString(), "Lớn nhất= {0:#,0}", DevExpress.Data.SummaryItemType.Max);
                            showFooter = true;
                            break;
                        default:
                            SummaryField(grid, dr["ID"].ToString(), "{0:#,0}", DevExpress.Data.SummaryItemType.None);
                            break;
                    }

                    switch (dr["HeaderAlign"].ToString().ToUpper())
                    {
                        case "CENTER":
                            grid.Columns[dr["ID"].ToString()].AppearanceHeader.TextOptions.HAlignment = Utils.HorzAlignment.Center;
                            break;
                        case "FAR":
                            grid.Columns[dr["ID"].ToString()].AppearanceHeader.TextOptions.HAlignment = Utils.HorzAlignment.Far;
                            break;
                        case "NEAR":
                            grid.Columns[dr["ID"].ToString()].AppearanceHeader.TextOptions.HAlignment = Utils.HorzAlignment.Near;
                            break;
                        default:
                            grid.Columns[dr["ID"].ToString()].AppearanceHeader.TextOptions.HAlignment = Utils.HorzAlignment.Default;
                            break;
                    }

                    switch (dr["DataAlign"].ToString().ToUpper())
                    {
                        case "CENTER":
                            grid.Columns[dr["ID"].ToString()].AppearanceCell.TextOptions.HAlignment = Utils.HorzAlignment.Center;
                            break;
                        case "FAR":
                            grid.Columns[dr["ID"].ToString()].AppearanceCell.TextOptions.HAlignment = Utils.HorzAlignment.Far;
                            break;
                        case "NEAR":
                            grid.Columns[dr["ID"].ToString()].AppearanceCell.TextOptions.HAlignment = Utils.HorzAlignment.Near;
                            break;
                        default:
                            grid.Columns[dr["ID"].ToString()].AppearanceCell.TextOptions.HAlignment = Utils.HorzAlignment.Default;
                            break;
                    }

                    if (dr["Sorted"].ToString().ToUpper() == "TRUE")
                        grid.Columns[dr["ID"].ToString()].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
                    else if (dr["Sorted"].ToString().ToUpper() == "FALSE")
                        grid.Columns[dr["ID"].ToString()].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                    else
                        grid.Columns[dr["ID"].ToString()].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.Default;

                    grid.Columns[dr["ID"].ToString()].Width = Convert.ToInt32(dr["Width"]);
                }

                foreach (DataRow drF in dtColumnVisible.Select("Visible = '" + true + "'" + " And Fixed ='LEFT'", "VisibleIndex ASC"))
                {
                    switch (drF["Fixed"].ToString().ToUpper())
                    {
                        case "LEFT":
                            grid.Columns[drF["ID"].ToString()].Fixed = FixedStyle.Left;
                            break;                       
                        default:
                            grid.Columns[drF["ID"].ToString()].Fixed = FixedStyle.None;
                            break;
                    }
                }

                foreach (DataRow drF in dtColumnVisible.Select("Visible = '" + true + "'" + " And Fixed ='RIGHT'", "VisibleIndex DESC"))
                {
                    switch (drF["Fixed"].ToString().ToUpper())
                    {
                        case "RIGHT":
                            grid.Columns[drF["ID"].ToString()].Fixed = FixedStyle.Right;
                            break;
                        default:
                            grid.Columns[drF["ID"].ToString()].Fixed = FixedStyle.None;
                            break;
                    }
                }

                //foreach (DataRow drF in drVisible)
                //{
                //    switch (drF["Fixed"].ToString().ToUpper())
                //    {
                //        case "LEFT":
                //            grid.Columns[drF["ID"].ToString()].Fixed = FixedStyle.Left;
                //            break;
                //        case "RIGHT":
                //            grid.Columns[drF["ID"].ToString()].Fixed = FixedStyle.Right;
                //            break;
                //        default:
                //            grid.Columns[drF["ID"].ToString()].Fixed = FixedStyle.None;
                //            break;
                //    }
                //}

                //ColumnAutoWidth
                if (drGridOption["ColumnAutoWidth"].ToString().ToUpper() == "TRUE")
                    grid.OptionsView.ColumnAutoWidth = true;
                else
                    grid.OptionsView.ColumnAutoWidth = false;

                //BestFitColumns
                if (drGridOption["BestFitColumns"].ToString().ToUpper() == "TRUE")
                    grid.BestFitColumns();

                grid.OptionsView.ShowFooter = showFooter;
            }
            catch { }
        }

        /// <summary>
        /// Cai dat luoi
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="drGridOption"></param>
        /// <param name="foreignLan"></param>
        public static void InitGridView(GridView grid, DataRow drGridOption, bool foreignLan)
        {
            try
            {
                //ShowAutoFilterRow
                if (drGridOption["ShowAutoFilterRow"].ToString().ToUpper() == "TRUE")
                    grid.OptionsView.ShowAutoFilterRow = true;
                else
                    grid.OptionsView.ShowAutoFilterRow = false;

                //MultiSelect
                if (drGridOption["MultiSelect"].ToString().ToUpper() == "TRUE")
                    grid.OptionsSelection.MultiSelect = true;
                else
                    grid.OptionsSelection.MultiSelect = false;

                //MultiSelectMode
                switch (drGridOption["MultiSelectMode"].ToString().ToUpper())
                {
                    case "CELL":
                        grid.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CellSelect;
                        break;
                    case "ROW":
                        grid.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;
                        break;
                    default:
                        grid.OptionsSelection.MultiSelectMode = GridMultiSelectMode.CheckBoxRowSelect;
                        break;
                }

                //ShowGroupPanel
                if (drGridOption["ShowGroupPanel"].ToString().ToUpper() == "TRUE")
                    grid.OptionsView.ShowGroupPanel = true;
                else
                    grid.OptionsView.ShowGroupPanel = false;

                //GroupPanelText
                if (foreignLan)
                    grid.GroupPanelText = "Drap a column header here to group by that column";
                else
                    grid.GroupPanelText = "Thả tiêu đề cột muốn nhóm vào đây";

                //NewItemRowPosition
                switch (drGridOption["NewItemRowPosition"].ToString().ToUpper())
                {
                    case "TOP":
                        grid.OptionsView.NewItemRowPosition = NewItemRowPosition.Top;
                        break;
                    case "BOTTOM":
                        grid.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
                        break;
                    default:
                        grid.OptionsView.NewItemRowPosition = NewItemRowPosition.None;
                        break;
                }

                //NewItemRowText
                if (foreignLan)
                    grid.NewItemRowText = "Click here to add a new row";
                else
                    grid.NewItemRowText = "Nhấn vào đây để thêm mới";                

                //ShowViewCaption
                if (drGridOption["ShowViewCaption"].ToString().ToUpper() == "TRUE")
                    grid.OptionsView.ShowViewCaption = true;
                else
                    grid.OptionsView.ShowViewCaption = false;

                //ViewCaption
                if (foreignLan)
                    grid.ViewCaption = drGridOption["ForeignGridName"].ToString();
                else
                    grid.ViewCaption = drGridOption["GridName"].ToString();               

                for (int i = 0; i < grid.Columns.Count; i++)
                    grid.Columns[i].Visible = false;

                grid.OptionsView.ShowFooter = false;
            }
            catch { }
        }

        /// <summary>
        /// Hien thi editor cho phep edit
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="pos"></param>
        public static void ShowEditor(GridView grid, NewItemRowPosition pos)
        {
            //Show edit
            grid.FocusedRowHandle = grid.RowCount - 1;
            grid.OptionsView.NewItemRowPosition = pos;
        }

        /// <summary>
        /// Tinh tong gia tri trong cot
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="fieldName"></param>
        /// <param name="displayFormat"></param>
        /// <param name="summaryType"></param>
        public static void SummaryField(GridView grid, string fieldName, string displayFormat, DevExpress.Data.SummaryItemType summaryType)
        {
            grid.OptionsView.ShowFooter = true;
            grid.Columns[fieldName].SummaryItem.FieldName = fieldName;
            grid.Columns[fieldName].SummaryItem.DisplayFormat = displayFormat;
            grid.Columns[fieldName].SummaryItem.SummaryType = summaryType;
        }

        /// <summary>
        /// Hien thi cac field trong luoi
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="fieldName"></param>
        /// <param name="caption"></param>
        public static void ShowField(GridView grid, string[] fieldName, string[] caption)
        {
            grid.OptionsView.ColumnAutoWidth = true;
            for (int i = 0; i < fieldName.Length; i++)
            {
                grid.Columns.AddField(fieldName[i]);
                grid.Columns[fieldName[i]].Visible = true;
                grid.Columns[fieldName[i]].Caption = caption[i];
                grid.Columns[fieldName[i]].OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains;
            }
        }

        /// <summary>
        /// Hien thi cac field trong luoi
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="fieldName"></param>
        /// <param name="caption"></param>
        /// <param name="width"></param>
        public static void ShowField(GridView grid, string[] fieldName, string[] caption, int[] width)
        {
            grid.OptionsView.ColumnAutoWidth = false;
            for (int i = 0; i < fieldName.Length; i++)
            {
                grid.Columns.AddField(fieldName[i]);
                grid.Columns[fieldName[i]].Visible = true;
                grid.Columns[fieldName[i]].Caption = caption[i];
                grid.Columns[fieldName[i]].Width = width[i];
                grid.Columns[fieldName[i]].OptionsFilter.AutoFilterCondition = AutoFilterCondition.Contains;
            }
        }

        /// <summary>
        /// Chi dinh cac filed khong duoc phep edit
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="fieldName"></param>
        public static void ReadOnlyColumn(GridView grid, string[] fieldName)
        {
            for (int i = 0; i < fieldName.Length; i++)
                grid.Columns[fieldName[i]].OptionsColumn.AllowEdit = false;
        }

        /// <summary>
        /// Khong cho phep edit toan bo cot tren luoi
        /// </summary>
        /// <param name="grid"></param>
        public static void ReadOnlyColumn(GridView grid)
        {
            for (int i = 0; i < grid.Columns.Count; i++)
                grid.Columns[i].OptionsColumn.AllowEdit = false;
        }

        /// <summary>
        /// Thiet lap co dinh cac field
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="fieldName"></param>
        /// <param name="style"></param>
        public static void FixedField(GridView grid, string[] fieldName, FixedStyle style)
        {
            for (int i = 0; i < fieldName.Length; i++)
                grid.Columns[fieldName[i]].Fixed = style;
        }

        /// <summary>
        /// Chi dinh cac field khong sort
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="fieldName"></param>
        public static void UnSortField(GridView grid, string[] fieldName)
        {
            for (int i = 0; i < fieldName.Length; i++)
                grid.Columns[fieldName[i]].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
        }

        /// <summary>
        /// Gan control RepositoryItem vao mot cell trong luoi
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="fieldName"></param>
        /// <param name="item"></param>
        public static void RegisterControlField(GridView grid, string fieldName, DevExpress.XtraEditors.Repository.RepositoryItem item)
        {
            grid.Columns[fieldName].ColumnEdit = item;
        }

        /// <summary>
        /// Gan control RepositoryItemDateEdit vao mot cell trong luoi
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="fieldName"></param>
        /// <param name="item"></param>
        public static void RegisterControlField(GridView grid, string fieldName, DevExpress.XtraEditors.Repository.RepositoryItemDateEdit item)
        {
            grid.Columns[fieldName].ColumnEdit = item;
        }

        /// <summary>
        /// Gan control RepositoryItemImageEdit vao mot cell trong luoi
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="fieldName"></param>
        /// <param name="item"></param>
        public static void RegisterControlField(GridView grid, string fieldName, DevExpress.XtraEditors.Repository.RepositoryItemImageEdit item)
        {
            grid.Columns[fieldName].ColumnEdit = item;
        }

        /// <summary>
        /// Gan control RepositoryItemTextEdit vao mot cell trong luoi
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="fieldName"></param>
        /// <param name="item"></param>
        public static void RegisterControlField(GridView grid, string fieldName, DevExpress.XtraEditors.Repository.RepositoryItemTextEdit item)
        {
            grid.Columns[fieldName].ColumnEdit = item;
        }

        /// <summary>
        /// Gan control RepositoryItemButtonEdit vao mot cell trong luoi
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="fieldName"></param>
        /// <param name="item"></param>
        public static void RegisterControlField(GridView grid, string fieldName, DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit item)
        {
            grid.Columns[fieldName].ColumnEdit = item;
        }   

        /// <summary>
        /// Gan control RepositoryItemLookUpEdit vao mot cell trong luoi
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="fieldName"></param>
        /// <param name="item"></param>
        public static void RegisterControlField(GridView grid, string fieldName, DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit item)
        {
            grid.Columns[fieldName].ColumnEdit = item;
        }

        /// <summary>
        /// Gan control RepositoryItemSpinEdit vao mot cell trong luoi
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="fieldName"></param>
        /// <param name="item"></param>
        public static void RegisterControlField(GridView grid, string fieldName, DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit item)
        {
            grid.Columns[fieldName].ColumnEdit = item;
        }

        /// <summary>
        /// Gan cac control RepositoryItem[] vao cac cell trong luoi
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="fieldName"></param>
        /// <param name="item"></param>
        public static void RegisterControlField(GridView grid, string[] fieldName, DevExpress.XtraEditors.Repository.RepositoryItem[] item)
        {
            for (int i = 0; i < fieldName.Length; i++)
                grid.Columns[fieldName[i]].ColumnEdit = item[i];
        }

        /// <summary>
        /// Dinh dang field trong luoi
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="fieldName"></param>
        /// <param name="align"></param>
        public static void AlignField(GridView grid, string[] fieldName, DevExpress.Utils.HorzAlignment align)
        {
            for (int i = 0; i < fieldName.Length; i++)
                grid.Columns[fieldName[i]].AppearanceCell.TextOptions.HAlignment = align;
        }

        /// <summary>
        /// Dinh dang tieu de cac field trong luoi
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="fieldName"></param>
        /// <param name="align"></param>
        public static void AlignHeader(GridView grid, string[] fieldName, DevExpress.Utils.HorzAlignment align)
        {
            for (int i = 0; i < fieldName.Length; i++)
                grid.Columns[fieldName[i]].AppearanceHeader.TextOptions.HAlignment = align;
        }

        /// <summary>
        /// Copy du lieu mot cell va paste vao cac cell co trong luoi
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="e"></param>
        public static void CopyCell(GridView grid, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.V)
            {
                IDataObject iData = Clipboard.GetDataObject();
                if (iData.GetDataPresent(DataFormats.UnicodeText))
                {
                    string text = (string)iData.GetData(DataFormats.UnicodeText);
                    Array.ForEach(grid.GetSelectedCells(), cell =>
                    {
                        try
                        {
                            cell.Column.View.SetRowCellValue(cell.RowHandle, cell.Column, text);
                            cell.Column.View.RefreshData();
                        }
                        catch
                        {
                            return;
                        }
                    });
                }
            }
        }

        /// <summary>
        /// Xoa cac dong da chon trong luoi
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="e"></param>
        public static void DeleteSelectedRows(GridView grid, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                if (grid.RowCount > 0)
                    if (grid.GetSelectedRows().Length > 0)
                        if (XtraMessageBox.Show(String.Format("Bạn có muốn xóa {0} dòng đã chọn không ?", grid.GetSelectedRows().Length), "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            grid.DeleteSelectedRows();
        }

        /// <summary>
        /// Xoa cac dong da chon trong luoi
        /// </summary>
        /// <param name="grid"></param>
        public static void DeleteSelectedRows(GridView grid)
        {
            if (grid.RowCount > 0)
                if (grid.GetSelectedRows().Length > 0)
                    if (XtraMessageBox.Show(String.Format("Bạn có muốn xóa {0} dòng đã chọn không ?", grid.GetSelectedRows().Length), "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        grid.DeleteSelectedRows();
        }

        /// <summary>
        /// Dinh dang field
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="fieldName"></param>
        /// <param name="formatType"></param>
        /// <param name="formatString"></param>
        public static void FormatDataField(GridView grid, string fieldName, DevExpress.Utils.FormatType formatType, string formatString)
        {
            if (formatType == DevExpress.Utils.FormatType.Custom)
                grid.Columns[fieldName].DisplayFormat.Format = new BaseFormatter();
            grid.Columns[fieldName].DisplayFormat.FormatString = formatString;
        }

        /// <summary>
        /// Dinh dang mau cho filed
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="fieldName"></param>
        /// <param name="foreColor"></param>
        public static void ForeColorFieldAppearance(GridView grid, string fieldName, Color foreColor)
        {
            grid.Columns[fieldName].AppearanceCell.ForeColor = foreColor;
        }

        /// <summary>
        /// Dinh dang background cho field
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="fieldName"></param>
        /// <param name="backColor"></param>
        public static void BackColorFieldAppearance(GridView grid, string fieldName, Color backColor)
        {
            grid.Columns[fieldName].AppearanceCell.BackColor = backColor;
        }

        /// <summary>
        /// Dinh dang background cho field
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="fieldName"></param>
        /// <param name="foreColor"></param>
        /// <param name="backColor"></param>
        /// <param name="font"></param>
        public static void FieldAppearance(GridView grid, string fieldName, Color foreColor, Color backColor, Font font)
        {
            grid.Columns[fieldName].AppearanceCell.ForeColor = foreColor;
            grid.Columns[fieldName].AppearanceCell.BackColor = backColor;
            grid.Columns[fieldName].AppearanceCell.Font = font;
        }

        /// <summary>
        /// Dinh dang field
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="fieldName"></param>
        /// <param name="font"></param>
        public static void FieldAppearance(GridView grid, string fieldName, Font font)
        {
            grid.Columns[fieldName].AppearanceCell.Font = font;
        }

        /// <summary>
        /// Dinh dang field voi dieu kien kem theo
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="fieldName"></param>
        /// <param name="condition"></param>
        /// <param name="color"></param>
        /// <param name="value"></param>
        public static void ConditionsAdjustment(GridView grid, string fieldName, FormatConditionEnum condition, Color color, object value)
        {
            StyleFormatCondition cn = new StyleFormatCondition(condition, grid.Columns[fieldName], null, value);
            cn.Appearance.BackColor = color;
            grid.FormatConditions.Add(cn);
        }

        /// <summary>
        /// Dinh dang field voi dieu kien ap dung cho row
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="fieldName"></param>
        /// <param name="condition"></param>
        /// <param name="applyToRow"></param>
        /// <param name="color"></param>
        /// <param name="value"></param>
        public static void ConditionsAdjustment(GridView grid, string fieldName, FormatConditionEnum condition, bool applyToRow, Color color, object value)
        {
            StyleFormatCondition cn = new StyleFormatCondition(condition, grid.Columns[fieldName], null, value);
            cn.Appearance.BackColor = color;
            cn.ApplyToRow = applyToRow;
            grid.FormatConditions.Add(cn);
        }

        /// <summary>
        /// lay dong filter hien hanh
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="view"></param>
        /// <returns></returns>
        public static List<T> GetFilteredData<T>(ColumnView view)
        {
            List<T> resp = new List<T>();
            for (int i = 0; i < view.DataRowCount; i++)
                resp.Add((T)view.GetRow(view.GetVisibleRowHandle(i)));
            return resp;
        }

        ///// <summary>
        ///// Kiem tra du lieu 1 field co bi trung hay khong
        ///// </summary>
        ///// <param name="grid"></param>
        ///// <param name="dt"></param>
        ///// <param name="fieldName"></param>
        ///// <param name="value"></param>
        ///// <param name="e"></param>
        ///// <param name="errorType"></param>
        //public static void DuplicateValidateRow(GridView grid, DataTable dt, string fieldName, object value, DevExpress.XtraEditors.DXErrorProvider.ErrorType errorType, ValidateRowEventArgs e)
        //{
        //    if (grid.Columns.View.ActiveFilter.IsEmpty)
        //    {
        //        if (dt.Select(string.Format("{0}='{1}'", fieldName, value)).Length > 1)
        //        {
        //            e.Valid = false;
        //            grid.Columns.View.SetColumnError(grid.Columns[fieldName], string.Format("{0} hiện tại đã có.", value), errorType);
        //        }
        //        else
        //            e.Valid = true;
        //    }
        //}

        ///// <summary>
        ///// Kiem tra du lieu 1 field co bi trung hay khong
        ///// </summary>
        ///// <param name="grid"></param>
        ///// <param name="count"></param>
        ///// <param name="fieldName"></param>
        ///// <param name="value"></param>
        ///// <param name="errorType"></param>
        ///// <param name="e"></param>
        //public static void DuplicateValidateRow(GridView grid, int count, string fieldName, object value, DevExpress.XtraEditors.DXErrorProvider.ErrorType errorType, ValidateRowEventArgs e)
        //{
        //    if (grid.Columns.View.ActiveFilter.IsEmpty)
        //    {
        //        if (count > 1)
        //        {
        //            e.Valid = false;
        //            grid.Columns.View.SetColumnError(grid.Columns[fieldName], string.Format("{0} hiện tại đã có.", value), errorType);
        //        }
        //        else
        //            e.Valid = true;
        //    }
        //}

        ///// <summary>
        ///// Kiem tra du lieu 1 field co bi trung hay khong
        ///// </summary>
        ///// <param name="grid"></param>
        ///// <param name="dt"></param>
        ///// <param name="fieldName"></param>
        ///// <param name="value"></param>
        ///// <param name="errorText"></param>
        ///// <param name="errorType"></param>
        ///// <param name="e"></param>
        //public static void DuplicateValidateRow(GridView grid, DataTable dt, string fieldName, object value, string errorText, DevExpress.XtraEditors.DXErrorProvider.ErrorType errorType, ValidateRowEventArgs e)
        //{
        //    if (grid.Columns.View.ActiveFilter.IsEmpty)
        //    {
        //        if (dt.Select(string.Format("{0}='{1}'", fieldName, value)).Length > 1)
        //        {
        //            e.Valid = false;
        //            grid.Columns.View.SetColumnError(grid.Columns[fieldName], errorText, errorType);
        //        }
        //        else
        //            e.Valid = true;
        //    }
        //}

        ///// <summary>
        ///// Kiem tra du lieu 1 field co bi trung hay khong
        ///// </summary>
        ///// <param name="grid"></param>
        ///// <param name="count"></param>
        ///// <param name="fieldName"></param>
        ///// <param name="value"></param>
        ///// <param name="errorText"></param>
        ///// <param name="errorType"></param>
        ///// <param name="e"></param>
        //public static void DuplicateValidateRow(GridView grid, int count, string fieldName, object value, string errorText, DevExpress.XtraEditors.DXErrorProvider.ErrorType errorType, ValidateRowEventArgs e)
        //{
        //    if (grid.Columns.View.ActiveFilter.IsEmpty)
        //    {
        //        if (count > 1)
        //        {
        //            e.Valid = false;
        //            grid.Columns.View.SetColumnError(grid.Columns[fieldName], errorText, errorType);
        //        }
        //        else
        //            e.Valid = true;
        //    }
        //}

        ///// <summary>
        ///// Kiem tra du lieu tren 1 field co bi trung lap khong truoc khi luu vao database
        ///// </summary>
        ///// <param name="grid"></param>
        ///// <param name="count"></param>
        ///// <param name="fieldName"></param>
        ///// <param name="value"></param>
        ///// <param name="errorText"></param>
        ///// <param name="errorType"></param>
        ///// <returns></returns>
        //public static bool IsDuplicateValidateRow(GridView grid, int count, string fieldName, object value, string errorText, DevExpress.XtraEditors.DXErrorProvider.ErrorType errorType)
        //{
        //    if (grid.Columns.View.ActiveFilter.IsEmpty)
        //    {
        //        if (count > 1)
        //        {
        //            grid.Columns.View.SetColumnError(grid.Columns[fieldName], errorText, errorType);
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        ///// <summary>
        ///// Kiem tra du lieu tren 1 field co bi trung lap khong truoc khi luu vao database
        ///// </summary>
        ///// <param name="grid"></param>
        ///// <param name="dt"></param>
        ///// <param name="fieldName"></param>
        ///// <param name="errorType"></param>
        ///// <returns></returns>
        //public static bool IsDuplicateValidateRow(GridView grid, DataTable dt, string fieldName, DevExpress.XtraEditors.DXErrorProvider.ErrorType errorType)
        //{
        //    Hashtable ht = new Hashtable();
        //    if (grid.Columns.View.ActiveFilter.IsEmpty)
        //    {
        //        foreach (DataRow r in dt.Rows)
        //        {
        //            if (!ht.Contains(r[fieldName]))
        //                ht.Add(r[fieldName], r[fieldName]);
        //            else
        //                grid.Columns.View.SetColumnError(grid.Columns[fieldName], string.Format("{0} hiện tại đã có.", r[fieldName]), errorType);
        //        }
        //    }
        //    return dt.Rows.Count == ht.Count;
        //}

        ///// <summary>
        ///// Kiem tra du lieu tren 1 field co bi trung lap khong truoc khi luu vao database
        ///// </summary>
        ///// <param name="grid"></param>
        ///// <param name="dt"></param>
        ///// <param name="fieldName"></param>
        ///// <param name="errorText"></param>
        ///// <param name="errorType"></param>
        ///// <returns></returns>
        //public static bool IsDuplicateValidateRow(GridView grid, DataTable dt, string fieldName, string errorText ,DevExpress.XtraEditors.DXErrorProvider.ErrorType errorType)
        //{
        //    Hashtable ht = new Hashtable();
        //    if (grid.Columns.View.ActiveFilter.IsEmpty)
        //    {
        //        foreach (DataRow r in dt.Rows)
        //        {
        //            if (!ht.Contains(r[fieldName]))
        //                ht.Add(r[fieldName], r[fieldName]);
        //            else
        //                grid.Columns.View.SetColumnError(grid.Columns[fieldName], errorText, errorType);
        //        }
        //    }
        //    return dt.Rows.Count == ht.Count;
        //}

        ///// <summary>
        ///// Kiem tra gia tri mot cot khong thong bao loi
        ///// </summary>
        ///// <param name="grid"></param>
        ///// <param name="dt"></param>
        ///// <param name="fieldName"></param>
        ///// <returns></returns>
        //public static bool IsDuplicateValidateRow(GridView grid, DataTable dt, string fieldName)
        //{
        //    Hashtable ht = new Hashtable();
        //    if (grid.Columns.View.ActiveFilter.IsEmpty)
        //    {
        //        foreach (DataRow r in dt.Rows)
        //        {
        //            if (!ht.Contains(r[fieldName]))
        //                ht.Add(r[fieldName], r[fieldName]);
        //        }
        //    }
        //    return dt.Rows.Count == ht.Count;
        //}

        ///// <summary>
        ///// Kiem tra trung o mot cot khong hien thi loi
        ///// </summary>
        ///// <param name="grid"></param>
        ///// <param name="dt"></param>
        ///// <param name="fieldName"></param>
        ///// <param name="value"></param>
        ///// <param name="e"></param>
        ///// <returns></returns>
        //public static bool IsDuplicateValidateRow(GridView grid, DataTable dt, string fieldName, object value, ValidateRowEventArgs e)
        //{
        //    if (grid.Columns.View.ActiveFilter.IsEmpty)
        //    {
        //        if (dt.Select(string.Format("{0}='{1}'", fieldName, value)).Length > 1)
        //            e.Valid = false;
        //        else
        //        {
        //            e.Valid = true;
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        /// <summary>
        /// An cac cot, dung sau phuong thuc ShowField
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="fieldName"></param>
        public static void HideField(GridView grid, string[] fieldName)
        {
            for (int i = 0; i < fieldName.Length; i++)
                grid.Columns[fieldName[i]].Visible = false;
        }

        /// <summary>
        /// Return data filter real
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable ReturnDataFilter(GridView grid, DataTable dt)
        {
            string filter = grid.ActiveFilterString;
            string sort = string.Empty;
            if (filter.Contains(".0000m"))
                filter = filter.Replace(".0000m", "");
            if (filter.Contains(".000m"))
                filter = filter.Replace(".000m", "");
            if (filter.Contains(".00m"))
                filter = filter.Replace(".00m", "");
            if (filter.Contains(".0m"))
                filter = filter.Replace(".0m", "");
            if (filter.Contains(".m"))
                filter = filter.Replace(".m", "");
            foreach (DevExpress.XtraGrid.Columns.GridColumn c in grid.SortedColumns)
            {
                switch (c.SortOrder)
                {
                    case DevExpress.Data.ColumnSortOrder.Ascending:
                        sort += string.Format("{0} ASC", c.FieldName);
                        break;
                    case DevExpress.Data.ColumnSortOrder.Descending:
                        sort += string.Format("{0} DESC", c.FieldName);
                        break;
                    default:
                        break;
                }
            }
            //if (!string.IsNullOrEmpty(sort))
            //    sort = sort.Remove(sort.Length - 2, 2);
            DataView dv = new DataView(dt, filter, sort, DataViewRowState.CurrentRows);
            return dv.ToTable();
        }
    }
}
