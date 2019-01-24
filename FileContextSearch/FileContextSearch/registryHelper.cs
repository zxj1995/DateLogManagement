using System;
using Microsoft.Win32;
using System.Windows.Forms;

public class registryHelper
{

    public static registryHelper registryHelperInstance;
    public static object locker = new object();
    private registryHelper()
    {
    }
    public static registryHelper GetInstance()
    {
        if (registryHelperInstance == null)
        {
            lock (locker)
            {
                if (registryHelperInstance == null)
                    registryHelperInstance = new registryHelper();
            }
        }
        return registryHelperInstance;
    }

    public string SoftName = "singleWaveTest";
    public string produceType = "Test";


    public string GetKeyValue(string name)
    {
        try
        {
            if (IsRegisted(name))
            {
                RegistryKey hkml = Registry.LocalMachine;
                RegistryKey software = hkml.OpenSubKey("SOFTWARE", RegistryKeyPermissionCheck.ReadSubTree).OpenSubKey(SoftName, RegistryKeyPermissionCheck.ReadSubTree).OpenSubKey(produceType, RegistryKeyPermissionCheck.ReadSubTree);
                if (software != null)
                {
                    RegistryKey regeditSub = software.OpenSubKey(name);
                    string registData = regeditSub.GetValue(name).ToString();
                    return registData;
                }
                return string.Empty;
            }
            else
                return "";
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
            return "";
        }
    }

    public void AddKey(string key, string keyValue) // 增加或修改
    {
        try
        {
            RegistryKey hklm = Registry.LocalMachine;
            if (registryHelper.GetInstance().IsRegisted(key))
            {
                RegistryKey Regedit = hklm.OpenSubKey("SOFTWARE", RegistryKeyPermissionCheck.ReadWriteSubTree).OpenSubKey(SoftName, RegistryKeyPermissionCheck.ReadWriteSubTree).OpenSubKey(produceType, RegistryKeyPermissionCheck.ReadWriteSubTree).OpenSubKey(key, RegistryKeyPermissionCheck.ReadWriteSubTree);
                Regedit.SetValue(key, keyValue);
            }
            else
            {
                RegistryKey RegeditParent = hklm.OpenSubKey("SOFTWARE", RegistryKeyPermissionCheck.ReadWriteSubTree).OpenSubKey(SoftName, RegistryKeyPermissionCheck.ReadWriteSubTree).OpenSubKey(produceType, RegistryKeyPermissionCheck.ReadWriteSubTree);
                RegistryKey Regedit = RegeditParent.CreateSubKey(key);
                Regedit.SetValue(key, keyValue);
            }
        }
        // Dim Regedit1 As RegistryKey = hklm.OpenSubKey("SOFTWARE", True)
        // If Regedit1 IsNot Nothing Then
        // Dim Regedit2 As RegistryKey = Regedit1.OpenSubKey(SoftName, True)
        // If Regedit2 IsNot Nothing Then
        // Dim Regedit3 As RegistryKey = Regedit2.OpenSubKey(produceType, True)
        // If Regedit3 IsNot Nothing Then
        // Dim Regedit4 As RegistryKey = Regedit3.OpenSubKey(key, True)
        // If Regedit4 IsNot Nothing Then
        // Regedit4.SetValue(key, keyValue)
        // Else
        // Regedit3.CreateSubKey(key)
        // registryHelperInstance.AddKey(key, keyValue)
        // End If
        // Else
        // Regedit2.CreateSubKey(produceType)
        // registryHelperInstance.AddKey(key, keyValue)
        // End If
        // Else
        // Regedit1.CreateSubKey(SoftName)
        // registryHelperInstance.AddKey(key, keyValue)
        // End If
        // Else
        // hklm.CreateSubKey("SOFTWARE")
        // registryHelperInstance.AddKey(key, keyValue)
        // End If

        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    public void DeleteKey(string key)
    {
        RegistryKey hkml = Registry.LocalMachine;
        RegistryKey software = hkml.OpenSubKey("SOFTWARE", true);
        if (software != null)
        {
            RegistryKey subKey = software.OpenSubKey(SoftName, true).OpenSubKey(produceType, true);
            if (subKey != null)
            {
                string[] aimnames = subKey.GetValueNames();
                foreach (string aimKey in aimnames)
                {
                    if (aimKey == key)
                        subKey.DeleteValue(key);
                }
            }
        }
    }

    public bool IsRegisted(string key)
    {
        try
        {
            // Dim hkml As RegistryKey = Registry.LocalMachine
            // Dim software As RegistryKey = hkml.OpenSubKey("SOFTWARE", True)

            // If software IsNot Nothing Then
            // Dim subKeys1 As RegistryKey = software.OpenSubKey(SoftName, True)
            // Dim subKeys2 As RegistryKey = subKeys1.OpenSubKey(produceType, True)

            // If subKeys2 IsNot Nothing Then
            // Dim keyNames As String() = subKeys2.GetValueNames()

            // For Each keyName As String In keyNames

            // If keyName = key Then
            // Return True
            // End If
            // Next
            // End If
            // End If


            RegistryKey hklm = Registry.LocalMachine;
            RegistryKey Regedit1 = hklm.OpenSubKey("SOFTWARE", true);
            if (Regedit1 != null)
            {
                RegistryKey Regedit2 = Regedit1.OpenSubKey(SoftName, true);
                if (Regedit2 != null)
                {
                    RegistryKey Regedit3 = Regedit2.OpenSubKey(produceType, true);
                    if (Regedit3 != null)
                    {
                        RegistryKey Regedit4 = Regedit3.OpenSubKey(key, true);
                        if (Regedit4 != null)
                            return true;
                        else
                            return false;
                    }
                    else
                    {
                        Regedit2.CreateSubKey(produceType);
                        registryHelperInstance.IsRegisted(key);
                    }
                }
                else
                {
                    Regedit1.CreateSubKey(SoftName);
                    registryHelperInstance.IsRegisted(key);
                }
            }
            else
            {
                hklm.CreateSubKey("SOFTWARE");
                registryHelperInstance.IsRegisted(key);
            }
        }

        catch
        {
            return false;
        }

        return false;
    }
}
