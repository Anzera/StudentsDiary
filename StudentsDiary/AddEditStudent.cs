using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentsDiary
{
    public partial class AddEditStudent : Form
    {


        private FileHelper<List<Student>> _fileHelper = 
            new FileHelper<List<Student>>(Program.FilePath);
        private int _studentId;
        private Student _student;
        public AddEditStudent(int id = 0)
        {
            InitializeComponent();
            _studentId = id;

            GetStudentsData();

            tbFirstName.Select();
        }
       
        private void GetStudentsData()
        {
            if (_studentId != 0)
            {
                Text = "Edydowanie ucznia";
                var students = _fileHelper.DeserializeFromFile();
                _student = students.FirstOrDefault(x => x.Id == _studentId);

                if (_student == null)
                    throw new Exception("Brak użytkownika o podanym Id");

                FillTextBoxes();
            }
        }

        private void FillTextBoxes()
        {
            tbId.Text = _student.Id.ToString();
            tbFirstName.Text = _student.FirstName;
            tbLastName.Text = _student.LastName;
            rtbComments.Text = _student.Comments;
            tbMath.Text = _student.Math;
            tbPhisics.Text = _student.Physics;
            tbPolishLang.Text = _student.PolishLang;
            tbTechnology.Text = _student.Technology;
            tbEnglishLang.Text = _student.EnglishLang;
            cbExtraWork.Checked = _student.ExtraWork;
            cboGroupId.SelectedItem = _student.GroupId;
        }
        private async void btnConfirm_Click(object sender, EventArgs e)
        {
            var students = _fileHelper.DeserializeFromFile();

            if (_studentId != 0)
                students.RemoveAll(x => x.Id == _studentId);
            else
                AssignIdToNewStudent(students);


            AddNewUserToLIst(students);

            _fileHelper.SerializeToFile(students);

            await LongProcessAsync();

            Close();
        }

        private async Task LongProcessAsync()
        {
            await Task.Run(() =>
                {
                    Thread.Sleep(3000);
                });
            
        }

        private void AddNewUserToLIst(List<Student> students)
        {
            var student = new Student
            {
                Id = _studentId,
                FirstName = tbFirstName.Text,
                LastName = tbLastName.Text,
                Comments = rtbComments.Text,
                Math = tbMath.Text,
                Physics = tbPhisics.Text,
                PolishLang = tbPolishLang.Text,
                EnglishLang = tbEnglishLang.Text,
                Technology = tbTechnology.Text,
                ExtraWork = cbExtraWork.Checked,
                GroupId = cboGroupId.SelectedItem.ToString()
            };

            students.Add(student);
        }
        private void AssignIdToNewStudent(List<Student> students)
        {
            var studentWithHighestId = students
                    .OrderByDescending(x => x.Id).FirstOrDefault();//sortowanie po polu Id

            _studentId = studentWithHighestId == null ?
                1 : studentWithHighestId.Id + 1;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
