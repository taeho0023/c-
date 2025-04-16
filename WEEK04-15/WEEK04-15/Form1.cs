using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Week04Homework
{
    public partial class FormManager : Form
    {
        //인스턴스 필드(변수), 멤버변수
        Department[] departments;
        List<Professor> professors;
        Dictionary<string, Student> students;

        List<Grade> testGrades;
        TextBox[] tbxTestScores;
        //생성자
        //인스턴스 생성시 초기화가 필요한 코드를
        //넣어준다.
        public FormManager()
        {
            InitializeComponent();
            departments = new Department[100];
            professors = new List<Professor>();
            students = new Dictionary<string, Student>();
            for (int i = 1; i <= 4; i++)
            {
                cmbYear.Items.Add(i);
            }
            cmbClass.Items
            .AddRange(new object[] { "A", "B", "C" });
            cmbRegStatus.Items.Add("재학");
            cmbRegStatus.Items.Add("졸업");
            cmbRegStatus.Items.Add("휴학");
            cmbRegStatus.Items.Add("퇴학");

            testGrades = new List<Grade>();
            tbxTestScores = new TextBox[]
            {
                tbxTestScore1,
                tbxTestScore2,
                tbxTestScore3,
                tbxTestScore4,
                tbxTestScore5,
                tbxTestScore6,
                tbxTestScore7,
                tbxTestScore8,
                tbxTestScore9,

            };
            departments[0] = new Department()
            {
                Code = "A001",
                Name = "컴퓨터정보"
            };
            departments[1] = new Department()
            {
                Code = "A002",
                Name = "컴퓨터시스템"
            };
            for (int i = 0; i < departments.Length; i++)
            {
                if (departments[i] != null)
                {
                    lbxDepartment.Items.Add(departments[i]);
                }
            }
            professors.Add(new Professor()
            {
                DepartmentCode = departments[0].Code,
                Number = "2020001",
                Name = "김인하"
            });
            professors.Add(new Professor()
            {
                DepartmentCode = departments[0].Code,
                Number = "2023003",
                Name = "김정석"
            });
            professors.Add(new Professor()
            {
                DepartmentCode = departments[1].Code,
                Number = "2023004",
                Name = "이공전"
            });
            students.Add("20240001", new Student()
            {
                Number = "20240001",
                Name = "김미영",
                RegStatus = "재학",
                Year = "1",
                BirthInfo = new DateTime(2004, 01, 01),
                DepartmentCode = "A001",
                AdvisorNumber = "2020001",
                Class = "B",
                Address = "인천 남구 용현동 100",
                Contact = "032-870-0000"
            });
            foreach (var student in students)
            {
                if (student.Value != null)
                {
                    lbxDictionary.Items.Add(student.Value);
                }
            }

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
            for (int i = 0; i < departments.Length; i++)
            {
                if (departments[i] == null)
                {

                    if (index < 0)
                    {
                        index = i;
                    }
                }
                else
                {
                    if (departments[i].Code == tbxDepartmentCode.Text)
                    {
                        MessageBox.Show("중복 되었습니다");
                        tbxDepartmentCode.Focus();
                        return;
                    }
                }

            }


            if (index < 0)
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
            if (lbxDepartment.SelectedIndex < 0)
            {
                MessageBox.Show("삭제할 과목을 선택하세요");
                return;
            }
            //is 연산자 , 타입 확인용으로 사용.
            if (lbxDepartment.SelectedItem is Department)
            {
                var target = (Department)lbxDepartment.SelectedItem;
                for (int i = 0; i < departments.Length; i++)
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
            switch (tabMain.SelectedIndex)
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
                    CmbDepartment.Items.Clear();
                    foreach (var department in departments)
                    {
                        if (department != null)
                        {
                            CmbDepartment.Items.Add(department);
                        }
                    }
                    ClearStudentInfo();
                    break;
                case 3:
                    ClearTestScoreInfo();
                    break;
                default:
                    break;
            }
        }

        private void cmbProfessorDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            //index값 검사해서 진행여부 결정
            if (cmbProfessorDepartment.SelectedIndex == -1)
            {

                MessageBox.Show("학과를 선택해주세요" +
                    "");
                return;
            }
            lbxProfessor.Items.Clear();
            //as 형변환 연산자.
            //형변환이 정상적이지 않으면 null변환.
            var department = cmbProfessorDepartment.SelectedItem as Department;

            if (department != null)
            {
                foreach (var professor in professors)
                {
                    if (professor != null && professor.DepartmentCode == department.Code)
                    {
                        lbxProfessor.Items.Add(professor);
                    }

                }
            }
        }

        private void btnRegisterProfessor_Click(object sender, EventArgs e)
        {
            if (tbxProfessorNumber.Text.Length == 0)
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
            if (false == cmbProfessorDepartment.SelectedItem is Department dept)
            {
                MessageBox.Show("학과정보에 이상 발생");
                cmbProfessorDepartment.Focus();
                return;
            }
            for (int i = 0; i < professors.Count; i++)
            {
                if (professors[i] != null && professors[i].Number == tbxProfessorNumber.Text)
                {
                    MessageBox.Show(" 이미 존재하는 교수님 입니다");
                    tbxProfessorNumber.Focus();
                    return;
                }
            }
            Professor prof = new Professor();
            prof.DepartmentCode = dept.Code;
            prof.Name = tbxProfessorName.Text;
            prof.Number = tbxProfessorNumber.Text;


            professors.Add(prof);
            lbxProfessor.Items.Add(prof);



        }

        private void btnRemoveProfessor_Click(object sender, EventArgs e)
        {
            if (lbxProfessor.SelectedIndex < 0)
            {
                MessageBox.Show("삭제할 과목을 선택하세요");
                return;
            }
            //is 연산자 , 타입 확인용으로 사용.
            if (lbxProfessor.SelectedItem is Professor)
            {
                var target = (Professor)lbxProfessor.SelectedItem;
                for (int i = 0; i < professors.Count; i++)
                {
                    if (professors[i] != null && professors[i] == target)
                    {
                        professors[i] = null;
                        break;
                    }
                }
                lbxProfessor.Items.Remove(target);//왜 이건지 확인하기
                lbxProfessor.SelectedIndex = -1;
            }
        }

        private void CmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbAdvisor.Items.Clear();
            if (CmbDepartment.SelectedIndex < 0)
            {
                return;
            }
            if (false == CmbDepartment.SelectedItem is Department dept)
            {
                return;
            }
            foreach (var professor in professors)
            {
                if (professor != null && professor.DepartmentCode ==
                dept.Code)
                {
                    CmbAdvisor.Items.Add(professor);
                }
            }
            CmbAdvisor.SelectedIndex = -1;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearStudentInfo();
        }
        private void ClearStudentInfo()
        {
            tbxNumber.Text = "20";
            tbxBirthYear.Text = "20";
            tbxName.Text = string.Empty;
            tbxBirthMonth.Text = "";
            tbxBirthDay.Text = "";
            CmbDepartment.SelectedIndex = -1;
            CmbAdvisor.SelectedIndex = -1;
            cmbYear.SelectedIndex = -1;
            cmbClass.SelectedIndex = -1;
            cmbRegStatus.SelectedIndex = -1;
            tbxAddress.Text = string.Empty;
            tbxContact.Text = string.Empty;

            tbxNumber.ReadOnly = false;
            selectedStudent = null;
            btnRegister.Text = "등록";
            lbxDictionary.SelectedIndex = -1;
        }
        Student selectedStudent = null;
        private void ClearTestScoreInfo()
        {
            tbxTestNumber.Text = "";
            foreach (var t in tbxTestScores)
            {
                t.Text = string.Empty;
            }
            lblTestName.Text = "";
            lblTestTotalCount.Text = "";
            lblTestAverage.Text = "";
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (selectedStudent == null)
            {
                RegisterStudent();
            }
            else
            {
                UpdateStudent();
            }
        }
        private void RegisterStudent()
        {
            if (tbxNumber.Text.Length != 8)
            {
                MessageBox.Show("학번이 8자리가 아닙니다");
                return;
            }
            if (tbxName.Text.Length < 2)
            {
                MessageBox.Show("이름이 두자리 미만입니다");
                return;
            }
            var number = tbxNumber.Text.Trim();
            //for, 성능 떨어짐
            for (int i = 0; i < students.Count; i++)
            {
                var pair = students.ElementAt(i);
                if (pair.Key == number)
                {
                    tbxNumber.Focus();
                    return;
                }
            }
            //foreach
            foreach (var pair in students)
            {
                if (pair.Key == number)
                {
                    tbxNumber.Focus();
                    return;
                }
            }
            //실제 많이 사용하는 방법1
            if (true == students.ContainsKey(number))
            {
                tbxNumber.Focus();
                return;
            }
            Student student = new Student();
            student.Number = number;
            student.Name = tbxName.Text.Trim();
            int birthYear, birthMonth;// birthDay;
            if (int.TryParse(tbxBirthYear.Text, out birthYear))
            {
                if (birthYear < 1900 || birthYear > 9000)
                {
                    MessageBox.Show("년도가 이상합니다");
                    return;
                }
            }
            else
            {
                return;
            }
            if (int.TryParse(tbxBirthMonth.Text, out birthMonth))
            {
                if (birthMonth < 1 || birthMonth > 12)
                {
                    MessageBox.Show("월 이상합니다");
                    return;
                }
            }
            else
            {
                return;
            }
            if (int.TryParse(tbxBirthDay.Text, out int birthDay))
            {
                if (birthMonth < 1 || birthMonth > 31)
                {
                    MessageBox.Show("일 이상합니다");
                    return;
                }
            }
            else
            {
                return;
            }
            //현재시간:DateTime.Now;
            student.BirthInfo
            = new DateTime(birthYear, birthMonth, birthDay);
            if (CmbDepartment.SelectedIndex < 0)
            {
                //cmbDepartment.Focus();
                //return;
                student.DepartmentCode = null;
            }
            else
            {
                student.DepartmentCode
                = (CmbDepartment.SelectedItem as Department).Code;
            }
            if (CmbAdvisor.SelectedIndex < 0)//지도교수를 설정한다. 선택한 지도교수가 없으면 null처리한다.
            {
                CmbAdvisor = null;
            }

            if (cmbYear.SelectedIndex < 0)//학년을 설정한다. 선택한 학년이 없으면 진행하지 않는다.
            {
                MessageBox.Show("");
                return;
            }
            if (cmbClass.SelectedIndex < 0) //반을 설정한다. 선택한 반이 없으면 진행하지 않는다.
            {
                MessageBox.Show("");
                return;
            }
            if (cmbRegStatus.SelectedIndex < 0) //재적상태를 설정한다. 선택한 재적상태이 없으면 진행하지 않는다.
            {
                MessageBox.Show("");
                return;
            }

            tbxAddress.Text = student.Address;
            tbxContact.Text = student.Contact;


            students[number] = student; //딕셔너리에 데이터 추가방법 1 또는 수정방법
                                        //students.Add(number, student);//딕셔너리에 데이터 추가방법 2
            lbxDictionary.Items.Add(student);
        }
        private void UpdateStudent()
        {


            if (selectedStudent == null)
            {
                MessageBox.Show("수정할 학생을 선택해주세요.");
                return;
            }


            if (tbxName.Text.Length < 2)
            {
                MessageBox.Show("이름은 두 글자 이상이어야 합니다.");
                return;
            }
            selectedStudent.Name = tbxName.Text.Trim();


            if (int.TryParse(tbxBirthYear.Text, out int birthYear) || birthYear < 1900 || birthYear > 9000)
            {
                MessageBox.Show("출생년도는 1900~9000 사이여야 합니다.");
                return;
            }

            if (int.TryParse(tbxBirthMonth.Text, out int birthMonth) || birthMonth < 1 || birthMonth > 12)
            {
                MessageBox.Show("출생월은 1~12 사이여야 합니다.");
                return;
            }

            if (int.TryParse(tbxBirthDay.Text, out int birthDay) || birthDay < 1 || birthDay > 31)
            {
                MessageBox.Show("출생일은 1~31 사이여야 합니다.");
                return;
            }

            selectedStudent.BirthInfo = new DateTime(birthYear, birthMonth, birthDay);


            if (CmbDepartment.SelectedIndex >= 0)
                selectedStudent.DepartmentCode = (CmbDepartment.SelectedItem as Department)?.Code;
            else
                selectedStudent.DepartmentCode = null;


            if (CmbAdvisor.SelectedIndex >= 0)
                selectedStudent.AdvisorNumber = (CmbAdvisor.SelectedItem as Professor)?.Number;
            else
                selectedStudent.AdvisorNumber = null;


            if (cmbYear.SelectedIndex < 0 || cmbClass.SelectedIndex < 0 || cmbRegStatus.SelectedIndex < 0)
            {
                MessageBox.Show("학년, 반, 재적상태를 모두 선택해주세요.");
                return;
            }

            selectedStudent.Year = cmbYear.SelectedItem.ToString();
            selectedStudent.Class = cmbClass.SelectedItem.ToString();
            selectedStudent.RegStatus = cmbRegStatus.SelectedItem.ToString();


            selectedStudent.Address = tbxAddress.Text.Trim();
            selectedStudent.Contact = tbxContact.Text.Trim();


            students[selectedStudent.Number] = selectedStudent;

            MessageBox.Show("수정이 완료되었습니다.");
            ClearStudentInfo();
            btnRegister.Text = "수정";

        }



        private void lbxDictionary_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxDictionary.SelectedIndex < 0)
            {
                return;
            }
            selectedStudent
            = (lbxDictionary.SelectedItem as Student);

            if (selectedStudent != null)
            {
                DisplaySelectedStudent(selectedStudent);
            }
        }
        private void DisplaySelectedStudent(Student student)
        {
            tbxNumber.ReadOnly = true;
            tbxNumber.Text = student.Number;
            tbxBirthYear.Text
            = student.BirthInfo.Year.ToString();
            tbxBirthMonth.Text = student.BirthInfo.Month.ToString();
            tbxBirthDay.Text = student.BirthInfo.Day.ToString();
            for (int i = 0; i < CmbDepartment.Items.Count; i++)
            {
                if ((CmbDepartment.Items[i] as Department).Code
                == student.DepartmentCode)
                {
                    CmbDepartment.SelectedIndex = i;
                    break;
                }
            }
            for (int i = 0; i < CmbAdvisor.Items.Count; i++)
            {
                if ((CmbAdvisor.Items[i] as Professor).Number == student.AdvisorNumber)
                {
                    CmbAdvisor.SelectedIndex = i;
                    break;
                }
            }
            for (int i = 0; i < cmbYear.Items.Count; i++)
            {
                if (cmbYear.Items[i].ToString() == student.Year)
                {
                    cmbYear.SelectedIndex = i;
                    break;
                }
            }
            for (int i = 0; i < cmbClass.Items.Count; i++)
            {
                if (cmbClass.Items[i].ToString() == student.Class)
                {
                    cmbClass.SelectedIndex = i;
                    break;
                }
            }
            for (int i = 0; i < cmbRegStatus.Items.Count; i++)
            {
                if (cmbRegStatus.Items[i].ToString() == student.RegStatus)
                {
                    cmbRegStatus.SelectedIndex = i;
                    break;
                }
            }
            tbxAddress.Text = student.Address;
            tbxContact.Text = student.Contact;
            btnRegister.Text = "수정";

        }



        private void btnTestSearchStudent_Click(object sender, EventArgs e)
        {


            ClearTestScoreInfo(); //(숙제) ClearTestScoreInfo()를 호출해서

            if (tbxTestNumber.Text == "" || tbxTestNumber.Text.Length != 8)
            {
                MessageBox.Show("학번을 입력하시오");
                return;
            }
            //(숙제) 학번이 입력되었는지 여부를 확인하고, 그에 맞는 처리를 진행한다.
            string number = tbxTestNumber.Text.Trim();
            selectedStudent = SearchStudentByNumber(number);


            if (selectedStudent == null)
            {
                MessageBox.Show("입력한 학번의 학생 정보가 없습니다.");
                return;
            }

            lblTestName.Text = selectedStudent.Name;
            //(숙제) 학번을 통해 검색한 학생 정보에서 이름을 가져와lblTestName에 설정한다.
            Grade grade = SearchGradeByNumber(selectedStudent.Number);

            if (grade != null)
            {
                for (int i = 0; i < grade.Scores.Count && i < tbxTestScores.Length; i++)
                {
                    tbxTestScores[i].Text
                        = grade.Scores[i].ToString("0.0");
                }
            }
        }

        private Grade SearchGradeByNumber(string number)
        {
            foreach (var grade in testGrades)
            {

                if (grade.StudentNumber == number)
                {
                    return grade;
                }
            }
            return null;
        }

        private Student SearchStudentByNumber(string number)
        {
            if (students.ContainsKey(number))
            {
                return students[number];
            }
            else
            {
                return null;
            }
        }

        private void tpgProfessor_Click(object sender, EventArgs e)
        {

        }

        private void btnTestRegScore_Click(object sender, EventArgs e)
        {
            if (selectedStudent == null)
            {
                tbxTestNumber.Focus();
                return;
            }

            //학생성적 입력한 항목 중 중간이 비어있으면 안됨
            for (int i = 1; i < tbxTestScores.Length; i++)
            {
                if (string.IsNullOrEmpty(tbxTestScores[i - 1].Text) && false == string.IsNullOrEmpty(tbxTestScores[i].Text))
                {

                }
            }
            var grade = SearchGradeByNumber(selectedStudent.Number);
            if (grade == null)
            {
                grade = new Grade();
                grade.StudentNumber = selectedStudent.Number;
            }
            grade.Scores.Clear();

            double temp;
            for (int i = 0; i < tbxTestScores.Length; i++)
            {
                if (string.IsNullOrEmpty(tbxTestScores[i].Text))
                {
                    break;
                }
                if (false == double.TryParse(tbxTestScores[i].Text,out temp))
                {
                    tbxTestScores[i].Focus();
                    return;
                }
                //grade
                grade.Scores.Add(temp);
            }
            testGrades.Add(grade);
            //총 과목수 출력
            lblTestTotalCount.Text = grade.Scores.Count.ToString();
            //, //평균 출력
            
            double average = grade.Average();
            
            //0이하가 return되면 진행하지 않도록
            //추후 코드 변경한다

            lblTestAverage.Text = average.ToString("F1");

        }
    }
}
        
        
    

