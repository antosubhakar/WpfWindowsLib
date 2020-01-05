﻿using System;
using System.Windows;
using System.Windows.Controls;
using WpfWindowsLib;


namespace SampleApp {


  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow: CheckedWindow {


    public MainWindow() {
      InitializeComponent();

      SaveButton.Click += saveButton_Click;
      CloseButton.Click += closeButton_Click;

      string?[] nullChangedValues = {null, "Changed"};
      NormalAutoCompleteBox.FilterMode = AutoCompleteFilterMode.Contains;
      NormalAutoCompleteBox.ItemsSource = nullChangedValues;
      NormalAutoCompleteBox.Init();
      RequiredAutoCompleteBox.FilterMode = AutoCompleteFilterMode.Contains;
      RequiredAutoCompleteBox.ItemsSource = nullChangedValues;
      RequiredAutoCompleteBox.Init(isRequired: true);
      ChangedCompleteBox.FilterMode = AutoCompleteFilterMode.Contains;
      ChangedCompleteBox.ItemsSource = nullChangedValues;
      ChangedCompleteBox.Init();
      ChangedCompleteBox.Text = "Changed";

      NormalCheckBox.Init();
      RequiredCheckBox.Init( isRequired: true);
      ChangedCheckBox.Init();
      ChangedCheckBox.IsChecked = true;

      string?[] emptyChangedValues = {"", "Changed"};
      NormalComboBox.ItemsSource = emptyChangedValues;
      NormalComboBox.Init();
      RequiredComboBox.ItemsSource = emptyChangedValues;
      RequiredComboBox.Init(isRequired: true);
      ChangedComboBox.ItemsSource = emptyChangedValues;
      ChangedComboBox.Init();
      ChangedComboBox.SelectedIndex = 1;

      NormalDatePicker.Init();
      RequiredDatePicker.Init(isRequired: true);
      ChangedDatePicker.Init();
      ChangedDatePicker.SelectedDate = DateTime.Now;

      NormalTextBox.Init();
      RequiredTextBox.Init(isRequired: true);
      ChangedTextBox.Init();
      ChangedTextBox.Text = "Changed";

      NormalDecimalTextBox.Init();
      RequiredDecimalTextBox.Init(isRequired: true);
      ChangedDecimalTextBox.Init();
      ChangedDecimalTextBox.DecimalValue = 123.45m;

      NormalEmailTextBox.Init();
      RequiredEmailTextBox.Init(isRequired: true);
      ChangedEmailTextBox.Init();
      ChangedEmailTextBox.Text = "Changed@nowhere.com";

      NormalIntTextBox.Init();
      RequiredIntTextBox.Init(isRequired: true);
      ChangedIntTextBox.Init();
      ChangedIntTextBox.IntValue = 123;
    }


    public override void OnICheckChanged(bool hasChanged) {
      updateSaveButtonIsEnabled();
    }


    private void updateSaveButtonIsEnabled() {
      SaveButton.IsEnabled = HasICheckChanged && IsAvailable;
    }


    public override void OnIsAvailableChanged(bool isAvailable) {
      updateSaveButtonIsEnabled();
    }


    private void saveButton_Click(object sender, RoutedEventArgs e) {
      throw new NotImplementedException();
    }


    private void closeButton_Click(object sender, RoutedEventArgs e) {
      //same code as in checkedWindow_Closing. Only needed in the sample to simulate closing.
      if (HasICheckChanged) {
        MessageBoxResult result;
        ShowChanged(true);
        result = MessageBox.Show("Press OK to close the Window without saving the data, Cancel to continue with data entry.",
        "Closing the Window ? Data has changed.", MessageBoxButton.OKCancel, MessageBoxImage.Exclamation, MessageBoxResult.Cancel);
        ShowChanged(false);
      }
    }
  }
}
