﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfWindowsLib {


  public class CheckedCheckBox: CheckBox, ICheck {
    public bool HasChanged { get; private set; }
    public event Action?  HasChangedEvent;
    public bool IsRequired { get; private set; }
    public bool IsAvailable { get; private set; }
    public event Action?  IsAvailableEvent;


    bool? initValue;
    Brush? defaultBackground;


    public void Init(bool? checkValue = null, bool isRequired = false) {

      initValue = checkValue;
      IsChecked = checkValue;
      IsRequired = isRequired;
      defaultBackground = Background;
      Checked += checkedCheckBox_Checked;
      Unchecked += checkedCheckBox_Checked;
      if (isRequired) {
        IsAvailable = checkValue.HasValue;
        showAvailability();
      }

      FrameworkElement element = this;
      do {
        element = (FrameworkElement)element.Parent;
        if (element==null) break;
        if (element is CheckedWindow window) {
          window.Register(this);
          break;
        }
      } while (true);
    }


    public void ResetHasChanged() {
      initValue = IsChecked;
      HasChanged = false;
    }


    private void checkedCheckBox_Checked(object sender, System.Windows.RoutedEventArgs e) {
      var newHasChanged = initValue!=IsChecked;
      if (HasChanged != newHasChanged) {
        HasChanged = newHasChanged;
        HasChangedEvent?.Invoke();
      }

      if (IsRequired) {
        var newIsAvailable = IsChecked.HasValue;
        if (IsAvailable!=newIsAvailable) {
          IsAvailable = newIsAvailable;
          showAvailability();
          IsAvailableEvent?.Invoke();
        }
      }
    }


    private void showAvailability() {
      if (!IsRequired) return;

      if (IsAvailable) {
        Background = defaultBackground;
      } else {
        Background = Styling.RequiredBrush;
      }
    }


    public void ShowChanged(bool isChanged) {
      if (HasChanged) {
        if (isChanged) {
          Background = Styling.HasChangedBackgroundBrush;
        } else {
          Background = defaultBackground;
        }
      }
    }
  }
}
