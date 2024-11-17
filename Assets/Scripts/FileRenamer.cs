using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Windows.Forms;
using Button = UnityEngine.UI.Button;

public class FileRenamer : MonoBehaviour
{
    public Button selectFileButton;
    public Button renameButton;
    //public InputField newNameInputField;
    public Text statusText;
    public Text pathText;

    public Dropdown mainDrop;

    public Transform season0;
    public InputField s0Input0;
    public InputField s0Input1;
    public Transform season1;
    public InputField s1Input0;
    public Dropdown s1Drop0;
    public Transform season2;
    public InputField s2Input0;
    public InputField s2Input1;

    private string selectedPath;

    void Start()
    {
        selectFileButton.onClick.AddListener(SelectFileOrFolder);
        renameButton.onClick.AddListener(RenameFiles);
        mainDrop.onValueChanged.AddListener((option) =>
        {
            season0.gameObject.SetActive(false);
            season1.gameObject.SetActive(false);
            season2.gameObject.SetActive(false);
            statusText.text = "";
            if(option == 0) season0.gameObject.SetActive(true);
            else if(option == 1) season1.gameObject.SetActive(true);
            else if(option == 2) season2.gameObject.SetActive(true);
        });
    }

    void SelectFileOrFolder()
    {
        using (var dialog = new FolderBrowserDialog())
        {
            dialog.Description = "��ѡ��Ҫ���������ļ���";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                selectedPath = dialog.SelectedPath;
                pathText.text = "��ѡ��·��: " + selectedPath;
            }
            else
            {
                pathText.text = "δѡ���κ�·��";
            }
        }
    }

    void RenameFiles()
    {
        if (string.IsNullOrEmpty(selectedPath))
        {
            statusText.text = "����ѡ��һ��·��";
            return;
        }

        if (mainDrop.value == 0)
        {
            //�滻
            SeasonAction0();
        }
        else if (mainDrop.value == 1)
        {
            //����
            SeasonAction1();
        }
        else if (mainDrop.value == 2)
        {
            //����
            SeasonAction2();
        }
            
    }

    private void SeasonAction0()
    {
        string oldName = s0Input0.text;
        string newName = s0Input1.text;
        if (string.IsNullOrEmpty(oldName))
        {
            statusText.text = "������Ҫ�滻���ַ���";
            return;
        }
        if (string.IsNullOrEmpty(newName))
        {
            statusText.text = "�������滻����ַ���";
            return;
        }
        try
        {
            string[] files = Directory.GetFiles(selectedPath);
            for (int i = 0; i < files.Length; i++)
            {
                string extension = Path.GetExtension(files[i]);
                string oldFileName = Path.GetFileNameWithoutExtension(files[i]);
                string newFileName = Path.Combine(selectedPath, oldFileName.Replace(oldName, newName) + extension);
                File.Move(files[i], newFileName);
            }

            statusText.text = "��������ɣ����������� " + files.Length + " ���ļ���";
        }
        catch (System.Exception ex)
        {
            statusText.text = "������ʧ��: " + ex.Message;
        }
    }

    private void SeasonAction1()
    {
        string newName = s1Input0.text;
        if (string.IsNullOrEmpty(newName))
        {
            statusText.text = "�������µ��ļ���";
            return;
        }
        int option = s1Drop0.value;
        try
        {
            string[] files = Directory.GetFiles(selectedPath);
            for (int i = 0; i < files.Length; i++)
            {
                string extension = Path.GetExtension(files[i]);
                string oldFileName = Path.GetFileNameWithoutExtension(files[i]);
                string newFileName;
                if (option == 0)
                    newFileName = Path.Combine(selectedPath, newName + oldFileName + extension);
                else newFileName = Path.Combine(selectedPath, oldFileName + newName + extension);
                File.Move(files[i], newFileName);
            }

            statusText.text = "��������ɣ����������� " + files.Length + " ���ļ���";
        }
        catch (System.Exception ex)
        {
            statusText.text = "������ʧ��: " + ex.Message;
        }
    }

    private void SeasonAction2()
    {
        string newName = s2Input0.text;
        if (string.IsNullOrEmpty(newName))
        {
            statusText.text = "�������µ��ļ���";
            return;
        }
        if (string.IsNullOrEmpty(s2Input1.text))
        {
            statusText.text = "�������µ��ļ���ʼ���";
            return;
        }
        int newNameIndex = int.Parse(s2Input1.text);

        try
        {
            string[] files = Directory.GetFiles(selectedPath);
            for (int i = 0; i < files.Length; i++)
            {
                string extension = Path.GetExtension(files[i]);
                string newFileName = Path.Combine(selectedPath, newName + newNameIndex + extension);
                newNameIndex++;
                File.Move(files[i], newFileName);
            }

            statusText.text = "��������ɣ����������� " + files.Length + " ���ļ���";
        }
        catch (System.Exception ex)
        {
            statusText.text = "������ʧ��: " + ex.Message;
        }
    }
}
