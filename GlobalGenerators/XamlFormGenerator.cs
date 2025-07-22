using BitMiracle.LibTiff.Classic;
using EasyITSystemCenter.GlobalClasses;
using EasyITSystemCenter.GlobalOperations;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;


namespace EasyITSystemCenter.GlobalGenerators {

    /// <summary>
    /// System Online Icon Generator from custom Path Its for working with Icon over Database Dynamically
    /// </summary>
    public class XamlFormGenerators {



        /// <summary>
        /// Xaml Form Generator 300,*  : 15 Rows
        /// This Is Generator For existing ListForm with Defined Grid Cool/Rows:
        /// Form Have Basic DataView And ListForm Empty Section
        /// Buttons,Tabs with WebView2 Window Are Defined By XAML Form
        /// Form is Automatically Translated
        /// </summary>
        /// <param name="rootGrid">The root grid.</param>
        /// <param name="dataset">The dataset.</param>
        /// <param name="systemCustomPageList">The system custom page list.</param>
        /// <returns>A Grid.</returns>
        public async static Task<Grid> StandardXamlFormGenerator(Grid rootGrid, DataTable dataset, SystemCustomPageList systemCustomPageList) {

            try {
                int columnNumber = 0;
                if (dataset != null) {

                    rootGrid.Children.Clear();

                    //TITLE
                    Label titleLabel = new Label() { Name = "lbl_" + systemCustomPageList.DbtableName, FontSize = 30, VerticalAlignment = VerticalAlignment.Top,
                        HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(0), Padding = new Thickness(0)
                    };
                    titleLabel.SetValue(Grid.RowProperty, 0); titleLabel.SetValue(Grid.ColumnProperty, 0); titleLabel.SetValue(Grid.ColumnSpanProperty, 2);
                    rootGrid.Children.Add(titleLabel);



                    foreach (var item in dataset.Columns) {
                        string fieldName = dataset.Columns[columnNumber].ToString().ToLower();
                        //Detect Field Type
                        var fieldTypeResult = ProgrammaticOperations.DetectTypeValueFromObject(dataset.Columns[columnNumber].Table.Select()[columnNumber].ItemArray[columnNumber]); bool columnDefined = false;

                        //DIAL TYPE 
                        if (fieldName.Contains("inherited")) {
                            ComboBox combobox = new ComboBox() {
                                Name = "frm_" + dataset.Columns[columnNumber].ToString().ToLower(),
                                VerticalAlignment = VerticalAlignment.Center,
                                HorizontalAlignment = HorizontalAlignment.Right,
                                Width = 250,
                                Margin = new Thickness(5),
                                ItemsSource = await DBOperations.LoadInheritedDataList(fieldName),
                                SelectedValuePath = "Name",
                                DisplayMemberPath = "Translation"
                            };
                            combobox.SetValue(Grid.RowProperty, columnNumber + 1); combobox.SetValue(Grid.ColumnProperty, 1);
                            rootGrid.Children.Add(combobox);
                            columnDefined = true;
                        }

                        if (fieldTypeResult.Item2 == "string" && !columnDefined
                            && fieldName != systemCustomPageList.ColumnName.ToLower() && !fieldName.Contains("inherited")) {
                            TextBox textbox = new TextBox() { Name = "frm_" + fieldName, VerticalAlignment = VerticalAlignment.Stretch,
                                HorizontalAlignment = HorizontalAlignment.Stretch, Margin = new Thickness(2), IsUndoEnabled = true
                                , AcceptsReturn = fieldName == "Description".ToLower(), TextWrapping = TextWrapping.Wrap,
                                VerticalScrollBarVisibility = ScrollBarVisibility.Auto
                            };
                            textbox.SetValue(Grid.RowProperty, columnNumber + 1); textbox.SetValue(Grid.ColumnProperty, 1);

                            //DESCRIPTION AREA
                            if (fieldName == "Description".ToLower()) { rootGrid.RowDefinitions[columnNumber + 1].SetCurrentValue(RowDefinition.HeightProperty, new GridLength(80, GridUnitType.Pixel)); }
                            rootGrid.Children.Add(textbox);
                            columnDefined = true;
                        }
                        if (fieldTypeResult.Item2 == "bool" && !columnDefined) {
                            CheckBox checkBox = new CheckBox() { Name = "frm_" + fieldName, VerticalAlignment = VerticalAlignment.Center,
                                HorizontalAlignment = HorizontalAlignment.Left, Margin = new Thickness(0, 5, 5, 5), Padding = new Thickness(0)
                            };
                            checkBox.SetValue(Grid.RowProperty, columnNumber + 1); checkBox.SetValue(Grid.ColumnProperty, 1);
                            rootGrid.Children.Add(checkBox);
                            columnDefined = true;
                        }
                        if (fieldTypeResult.Item2 == "int" && fieldName != "userid" && !columnDefined) {
                            NumericUpDown numeric = new NumericUpDown() { Name = "frm_" + fieldName,
                                VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Stretch, Margin = new Thickness(2)
                                , IsReadOnly = fieldName.ToLower() == "id".ToLower()
                            };
                            numeric.SetValue(Grid.RowProperty, columnNumber + 1); numeric.SetValue(Grid.ColumnProperty, 1);
                            rootGrid.Children.Add(numeric);
                            columnDefined = true;
                        }
                        if (fieldTypeResult.Item2 == "double" && fieldName != "userid" && !columnDefined) {
                            NumericUpDown numeric = new NumericUpDown() {
                                Name = "frm_" + fieldName, VerticalAlignment = VerticalAlignment.Center,
                                HorizontalAlignment = HorizontalAlignment.Stretch,
                                Margin = new Thickness(2),
                                IsReadOnly = dataset.Columns[columnNumber].ToString().ToLower() == "id".ToLower()
                            };
                            numeric.SetValue(Grid.RowProperty, columnNumber + 1); numeric.SetValue(Grid.ColumnProperty, 1);
                            rootGrid.Children.Add(numeric);
                            columnDefined = true;
                        }
                        if (fieldTypeResult.Item2 == "time" && !columnDefined) {
                            DateTimePicker datePicker = new DateTimePicker() {
                                Name = "frm_" + fieldName,
                                VerticalAlignment = VerticalAlignment.Center,
                                HorizontalAlignment = HorizontalAlignment.Stretch,
                                Margin = new Thickness(2), IsTodayHighlighted = true,
                                IsClockVisible = true, PickerVisibility = TimePartVisibility.All,
                                SelectedDateFormat = DatePickerFormat.Short,
                                SelectedTimeFormat = TimePickerFormat.Short
                            };
                            datePicker.SetValue(Grid.RowProperty, columnNumber + 1); datePicker.SetValue(Grid.ColumnProperty, 1);
                            rootGrid.Children.Add(datePicker);
                            columnDefined = true;
                        }
                        if (fieldTypeResult.Item2 == "date" && !columnDefined) {
                            DateTimePicker datePicker = new DateTimePicker() {
                                Name = "frm_" + fieldName,
                                VerticalAlignment = VerticalAlignment.Center,
                                HorizontalAlignment = HorizontalAlignment.Stretch,
                                Margin = new Thickness(2), IsTodayHighlighted = true,
                                IsClockVisible = false,
                                SelectedDateFormat = DatePickerFormat.Short
                            };
                            datePicker.SetValue(Grid.RowProperty, columnNumber + 1); datePicker.SetValue(Grid.ColumnProperty, 1);
                            rootGrid.Children.Add(datePicker);
                            columnDefined = true;
                        }

                        if (fieldTypeResult.Item2 == "datetime" && !columnDefined && !fieldName.StartsWith("timestamp")) {
                            DateTimePicker datePicker = new DateTimePicker() {
                                Name = "frm_" + fieldName,
                                VerticalAlignment = VerticalAlignment.Center,
                                HorizontalAlignment = HorizontalAlignment.Stretch,
                                Margin = new Thickness(2),
                                IsTodayHighlighted = true,
                                IsClockVisible = true,
                                PickerVisibility = TimePartVisibility.All,
                                SelectedDateFormat = DatePickerFormat.Short
                            };
                            datePicker.SetValue(Grid.RowProperty, columnNumber + 1); datePicker.SetValue(Grid.ColumnProperty, 1);
                            rootGrid.Children.Add(datePicker);
                            columnDefined = true;
                        }


                        //Insert Label for Each Field Without EDITOR AND HIDDEN FIELDS
                        if (columnDefined
                            && fieldName != systemCustomPageList.ColumnName.ToLower() && fieldName != "userid" && fieldName != "timestamp") {
                            Label fieldLabel = new Label() { Name = "lbl_" + fieldName, FontSize = 20,
                                VerticalAlignment = VerticalAlignment.Stretch,
                                VerticalContentAlignment = VerticalAlignment.Top,
                                HorizontalAlignment = HorizontalAlignment.Right,
                                Margin = new Thickness(5, 0, 5, 5),
                                Padding = new Thickness(0)
                            };
                            fieldLabel.SetValue(Grid.RowProperty, columnNumber + 1); fieldLabel.SetValue(Grid.ColumnProperty, 0);
                            rootGrid.Children.Add(fieldLabel);
                        }

                        columnNumber += 1;
                    };

                }
            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

            await FormOperations.TranslateFormFields(rootGrid);
            return rootGrid;
        }


        /// <summary>
        /// Control The Data Comunication Between Generic Form And Selected Record
        /// </summary>
        /// <param name="userForm"></param>
        /// <param name="selectedRecord"></param>
        /// <param name="recordForSave"></param>
        /// <param name="dataToForm"></param>
        /// <param name="systemCustomPageSetting"></param>
        /// <param name="newRec"></param>
        /// <param name="copy"></param>
        /// <returns></returns>
        public static string StandardXamlFormIODataController(ref Grid userForm, ref DataRowView selectedRecord, ref List<Dictionary<string, string>> recordForSave, bool dataToForm, SystemCustomPageList systemCustomPageSetting, bool newRec, bool copy) {
            int index = 0; string dataForBrowser = null; recordForSave.Clear();

            // DATA TO FORM

            var columnList = selectedRecord.Row.Table.Columns;
            foreach (object prop in columnList) {

                bool setDone = false;
                string fieldName = ((DataColumn)prop).ColumnName.ToLower();
                string fieldValue = selectedRecord.Row.ItemArray[index].ToString();
                if (newRec) { fieldValue = null; }
                var fieldTypeResult = ProgrammaticOperations.DetectTypeValueFromObject(selectedRecord.Row.ItemArray[index]);

                try {
                    if (dataToForm) {

                        #region SpecialFieldsConditions

                        //SPECIFIC CONDITION MUST BE FIRST
                        if (!setDone && systemCustomPageSetting.ColumnName != null && fieldName == systemCustomPageSetting.ColumnName.ToLower()) {
                            dataForBrowser = fieldValue; setDone = true;
                        }
                        //0 ID for Copy
                        if (!setDone && (copy || newRec) && systemCustomPageSetting.ColumnName != null && fieldName == "Id".ToLower()) {

                            try {
                                fieldValue = "0";
                                if (userForm.FindChild<NumericUpDown>($"frm_{fieldName}") != null) {
                                    userForm.FindChild<NumericUpDown>($"frm_{fieldName}").SetCurrentValue(NumericUpDown.ValueProperty, (double?)int.Parse(fieldValue));
                                    setDone = true;
                                }
                            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

                        }
                        if (!setDone && fieldName.Contains("inherited")) { //Combobox

                            try {
                                if (userForm.FindChild<ComboBox>($"frm_{fieldName}") != null) {
                                    userForm.FindChild<ComboBox>($"frm_{fieldName}").SetCurrentValue(System.Windows.Controls.Primitives.Selector.SelectedValueProperty, fieldValue);
                                    setDone = true;
                                }
                            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }
                        }
                        if (!setDone && fieldName == "timestamp") {
                            setDone = true;
                            //userForm.Children.AsParallel().OfType<DateTimePicker>().Where(a => a.Name.ToLower().Split('_')[1] == fieldName).
                            //First().SetCurrentValue(DateTimePicker.SelectedDateProperty, DateTime.Parse(fieldValue));
                        }
                        if (!setDone && fieldName == "userid") {
                            setDone = true;
                            //userForm.Children.AsParallel().OfType<NumericUpDown>().Where(a => a.Name.ToLower().Split('_')[1] == fieldName).
                            //First().SetCurrentValue(NumericUpDown.ValueProperty, double.Parse(fieldValue));
                        }

                        #endregion SpecialFieldsConditions

                        //STANDARD GENERATION PART
                        if (!setDone && new string[] { "int", "float", "double", "numeric", "int32", "int64" }.Contains(fieldTypeResult.Item2)) {
                            try {
                                if (userForm.FindChild<NumericUpDown>($"frm_{fieldName}") != null) {
                                    userForm.FindChild<NumericUpDown>($"frm_{fieldName}").SetCurrentValue(NumericUpDown.ValueProperty, fieldValue == null ? null : double.Parse(fieldValue) as double?);
                                    setDone = true;
                                }
                            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

                        } else if (!setDone && new string[] { "date", "time", "datetime" }.Contains(fieldTypeResult.Item2)) {

                            try {
                                if (userForm.FindChild<DateTimePicker>($"frm_{fieldName}") != null) {
                                    userForm.FindChild<DateTimePicker>($"frm_{fieldName}").SetCurrentValue(DateTimePicker.SelectedDateProperty, fieldValue == null ? null : DateTime.Parse(fieldValue) as DateTime?);
                                    setDone = true;
                                }
                            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

                        } else if (!setDone && new string[] { "bool" }.Contains(fieldTypeResult.Item2)) {

                            try {
                                if (userForm.FindChild<CheckBox>($"frm_{fieldName}") != null) {
                                    userForm.FindChild<CheckBox>($"frm_{fieldName}").SetCurrentValue(System.Windows.Controls.Primitives.ToggleButton.IsCheckedProperty, fieldValue == null ? false : bool.Parse(fieldValue));
                                    setDone = true;
                                }
                            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

                        } else if (!setDone && new string[] { "string" }.Contains(fieldTypeResult.Item2)) {

                            try {
                                if (userForm.FindChild<TextBox>($"frm_{fieldName}") != null) {
                                    userForm.FindChild<TextBox>($"frm_{fieldName}").SetCurrentValue(TextBox.TextProperty, fieldValue);
                                    setDone = true;
                                }
                            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

                        }

                    } else { //PREPARE DATA for Save
                        if (!setDone && fieldName == "userid") { 
                            try {
                                fieldValue = App.UserData.Authentification.Id.ToString();
                                setDone = true;
                            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

                        } else if (!setDone && fieldName == "timestamp") {
                            try {
                                fieldValue = DateTimeOffset.Now.DateTime.ToString();
                                setDone = true;
                            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

                        } else if (!setDone && fieldName.Contains("inherited")) { //Combobox
                            try {
                                if (userForm.FindChild<ComboBox>($"frm_{fieldName}") != null) {
                                    fieldValue = (string)userForm.FindChild<ComboBox>($"frm_{fieldName}").SelectedValue;
                                    setDone = true;
                                }
                            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

                        } else if (!setDone && new string[] { "int", "float", "double", "numeric", "int32", "int64" }.Contains(fieldTypeResult.Item2)) {

                            try {
                                if (userForm.FindChild<NumericUpDown>($"frm_{fieldName}") != null) {
                                    fieldValue = userForm.FindChild<NumericUpDown>($"frm_{fieldName}").Value.ToString();
                                    setDone = true;
                                }
                            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

                        } else if (!setDone && new string[] { "date", "time", "datetime" }.Contains(fieldTypeResult.Item2)) {

                            try {
                                if (userForm.FindChild<DateTimePicker>($"frm_{fieldName}") != null) {
                                    fieldValue = userForm.FindChild<DateTimePicker>($"frm_{fieldName}").SelectedDate.ToString();
                                    setDone = true;
                                }
                            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

                        } else if (!setDone && new string[] { "bool" }.Contains(fieldTypeResult.Item2)) {

                            try {
                                if (userForm.FindChild<CheckBox>($"frm_{fieldName}") != null) {
                                    fieldValue = userForm.FindChild<CheckBox>($"frm_{fieldName}").IsChecked.ToString();
                                    setDone = true;
                                }
                            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

                        } else if (!setDone && new string[] { "string" }.Contains(fieldTypeResult.Item2)) {

                            try {
                                if (userForm.FindChild<TextBox>($"frm_{fieldName}") != null) {
                                    fieldValue = userForm.FindChild<TextBox>($"frm_{fieldName}").Text;
                                    setDone = true;
                                }
                            } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

                        }
                    }

                } catch (Exception autoEx) { App.ApplicationLogging(autoEx); }

                if (!dataToForm && fieldName.ToLower().Contains("content"))
                { recordForSave.Add(new Dictionary<string, string>() { { fieldName, "DATACONTENT_FORREPLACE" } }); }
                else { recordForSave.Add(new Dictionary<string, string>() { { fieldName, fieldValue } }); }

                index += 1;

            }

            if (dataToForm) { return dataForBrowser; } else { return recordForSave.ObjectToJson();}

        }

    }
}