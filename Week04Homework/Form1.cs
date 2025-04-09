using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Week04Homework
{
    public partial class FormManager: Form
    {
        //인스턴스 필드(변수), 멤버변수
        Department[] departments;
        List<Professor> professors;

        //생성자
        //인스턴스 생성시 초기화가 필요한 코드를
        //넣어준다.
        public FormManager()
        {
            InitializeComponent();
            departments = new Department[100];
            professors = new List<Professor>();
        }
        
        private void btnRegisterDepartment_Click(object sender, EventArgs e)
        {
            //학과코드랑 이름이 비어있으면 안됨.
            if (tbxDepartmentCode.Text.Length == 0)
            {
                MessageBox.Show("학과코드가 비어있습니다");
                tbxDepartmentCode.Focus();
                return;
            }
            if (tbxDepartmentName.Text.Length == 0)
            {
                MessageBox.Show("학과이름이 비어있습니다");
                tbxDepartmentName.Focus();
                return;
            }


            int index = -1;
            for(int i=0; i<departments.Length; i++) {
                if (departments[i] == null)
                {

                    if (index < 0)
                    {
                        index = i;
                    }
                }
                else {
                  if (departments[i].Code == tbxDepartmentCode.Text){
                        MessageBox.Show("중복 되었습니다");
                        tbxDepartmentCode.Focus();
                        return;
                   }
                }
                     
            }
        
            
            if(index < 0)
            {
                MessageBox.Show("학과가 가득 찼습니다");
                return;
            }

            Department dept = new Department();
            dept.Code = tbxDepartmentCode.Text;
            dept.Name = tbxDepartmentName.Text;

            departments[index] = dept;

            lbxDepartment.Items.Add(dept);
            //lbxDepartment.Items.Add(dept.Code);
            //lbxDepartment.Items.Add(dept.Name);
            //lbxDepartment.Items.Add($"[{dept.Code}] {dept.Name}");

        }

        private void btnRemoveDepartment_Click(object sender, EventArgs e)
        {
            if (lbxDepartment.SelectedIndex < 0) {
                MessageBox.Show("삭제할 과목을 선택하세요");
                return;
            }
            //is 연산자 , 타입 확인용으로 사용.
            if(lbxDepartment.SelectedItem is Department) {
                var target = (Department)lbxDepartment.SelectedItem;
                for (int i=0; i < departments.Length; i++)
                {
                    if (departments[i] != null && departments[i] == target)
                    {
                        departments[i] = null;
                        break;
                    }
                }
                lbxDepartment.Items.RemoveAt(lbxDepartment.SelectedIndex);
                lbxDepartment.SelectedIndex = -1;
            }
        }

        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(tabMain.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    cmbProfessorDepartment.Items.Clear();
                    foreach (var department in departments)
                    {
                        
                        if (department != null)
                        {
                            cmbProfessorDepartment.Items.Add(department);

                        }

                        cmbProfessorDepartment.SelectedIndex = -1;
                        lbxProfessor.Items.Clear();
                    }
                    break;
                case 2:
                    break;
                default:
                    break;
            }
        }

        private void cmbProfessorDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            //index값 검사해서 진행여부 결정
            if(cmbProfessorDepartment.SelectedIndex == -1)
            {
                
                MessageBox.Show("학과를 선택해주세요" +
                    "");
                return;
            }
            lbxProfessor.Items.Clear();
            //as 형변환 연산자.
            //형변환이 정상적이지 않으면 null변환.
            var department = cmbProfessorDepartment.SelectedItem as Department;

            if(department != null) {
                foreach (var professor in professors) {
                    if (professor != null && professor.DepartmentCode == department.Code)  {
                        lbxProfessor.Items.Add(professor);
                    }
                   
                }
            }
        }

        private void btnRegisterProfessor_Click(object sender, EventArgs e)
        {
           if(tbxProfessorNumber.Text.Length == 0)
            {
                MessageBox.Show("교수번호를 입력하세요");
                tbxProfessorNumber.Focus();
                return;
            }
            if (tbxProfessorName.Text.Length == 0)
            {
                MessageBox.Show("교수이름을 입력하세요");
                tbxProfessorName.Focus();
                return;
            }
            if (cmbProfessorDepartment.Text.Length == 0)
            {
                MessageBox.Show("학과를 선택해주세요.");
                cmbProfessorDepartment.Focus();
                return;
            }

        }

        private void btnRemoveProfessor_Click(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {

        }

        private void lbxDictionary_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lbxDictionary.SelectedIndex < 0)
            {
                return;
            }

            // selectedStudent = (lbxDictionary.SelectedItem as Student);

            btnNew_Click(sender, EventArgs.Empty);

            if (selectedstudemnt != null){
                DisplaySelectedstudent(selectedStudent);
            }
        }

        private void DisplaySelectedstudent(object student)
        {
            tbxNumber.ReadOnly = true;
            tbxNumber.Text = student.Number;
            tbxBirthYear.Text
                =
        }
    }
        }

//실제 많이 사용하는 방법
/*if (true == student.s.Containskey(number))
{
tbxNumber.Focus();
return;
}*

Student student = new Student();
Student.Number = number;
Student.Name = tbxName.Text.Trim();

int birthYear, birthMonth, birthDay;
if (int.TryParse(tbxBirthYear.Text, out birthYear)){
  //
} else {
  return;
}
if (int.TryParse(tbxBirthYear.Text, out birthMonth)){
    //
} else {
    return;
}
if (int.TryParse(tbxBirthYear.Text, out int birthDay)){
    //
} else {
    return;
}
//현재시간dateTime.Now;
student.BirthInfo = new DateTime(birthYear, birthMonth, birthDay);

if(cmbDepartment.SelectedIndex <0) {
//cmbDepartment .Focus();
//return;
student.DepartmentCode = null;

} else {
student.DepartmentCode
= (cmbDepartment.SelectedItem as Department).Code;
}

students[numder] = student; //딕셔너리에 데이터 추가방법1
//students.Add(number, student);//딕셔너리에 데이터 추가방법2
lbxDictionary.Items.Add(student);*/