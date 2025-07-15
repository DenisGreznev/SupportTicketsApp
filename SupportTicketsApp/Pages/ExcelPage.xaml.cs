using Microsoft.Win32;
using OfficeOpenXml;
using SupportTicketsApp.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SupportTicketsApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для ExcelPage.xaml
    /// </summary>
    public partial class ExcelPage : Page
    {
        public ExcelPage()
        {
            InitializeComponent();
            LoadReportTypes();
        }
        private void LoadReportTypes()
        {
            ReportTypeComboBox.Items.Add("Отчет по заявкам");
            ReportTypeComboBox.Items.Add("Отчет по открытым заявкам");
            ReportTypeComboBox.Items.Add("Отчет по закрытым заявкам");
            ReportTypeComboBox.Items.Add("Отчет по клиентам");
            ReportTypeComboBox.Items.Add("Отчет по сотрудникам");
        }

        private void GenerateReportButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedReportType = ReportTypeComboBox.SelectedItem as string;

            if (string.IsNullOrEmpty(selectedReportType))
            {
                MessageBox.Show("Пожалуйста, выберите тип отчета.");
                return;
            }

            switch (selectedReportType)
            {
                case "Отчет по заявкам":
                    GenerateTicketsReport();
                    break;
                case "Отчет по открытым заявкам":
                    GenerateOpenTicketsReport();
                    break;
                case "Отчет по закрытым заявкам":
                    GenerateClosedTicketsReport();
                    break;
                case "Отчет по клиентам":
                    GenerateClientsReport();
                    break;
                case "Отчет по сотрудникам":
                    GenerateEmployeesReport();
                    break;
                default:
                    MessageBox.Show("Неизвестный тип отчета.");
                    break;
            }
        }

        private void GenerateTicketsReport()
        {
            using (var db = new ApplicationDbContext())
            {
                // Eager loading to include related data
                var tickets = db.Tickets
                    .Include(t => t.Client)
                    .Include(t => t.Employee)
                    .Include(t => t.Status)
                    .ToList();

                if (tickets != null && tickets.Any())
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        string filePath = saveFileDialog.FileName;

                        try
                        {
                            using (ExcelPackage excelPackage = new ExcelPackage())
                            {
                                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Заявки");

                                // Заголовки
                                worksheet.Cells[1, 1].Value = "ID заявки";
                                worksheet.Cells[1, 2].Value = "Заголовок";
                                worksheet.Cells[1, 3].Value = "Описание";
                                worksheet.Cells[1, 4].Value = "Сотрудник";
                                worksheet.Cells[1, 5].Value = "Клиент";
                                worksheet.Cells[1, 6].Value = "Метод регистрации";
                                worksheet.Cells[1, 7].Value = "Статус";
                                worksheet.Cells[1, 8].Value = "Дата создания";

                                // Заполнение данными
                                for (int i = 0; i < tickets.Count; i++)
                                {
                                    worksheet.Cells[i + 2, 1].Value = tickets[i].TicketId;
                                    worksheet.Cells[i + 2, 2].Value = tickets[i].Title;
                                    worksheet.Cells[i + 2, 3].Value = tickets[i].Description;
                                    worksheet.Cells[i + 2, 4].Value = tickets[i].Employee?.FullName;
                                    worksheet.Cells[i + 2, 5].Value = tickets[i].Client?.ClientName;
                                    worksheet.Cells[i + 2, 6].Value = tickets[i].RegistrationMethod;
                                    worksheet.Cells[i + 2, 7].Value = tickets[i].Status?.StatusName;
                                    worksheet.Cells[i + 2, 8].Value = tickets[i].CreatedDate.ToString();
                                }

                                FileInfo excelFile = new FileInfo(filePath);
                                excelPackage.SaveAs(excelFile);

                                MessageBox.Show("Отчет по заявкам успешно создан: " + filePath);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при создании отчета: {ex.Message}");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Нет данных для отчета по заявкам.");
                }
            }
        }

        private void GenerateOpenTicketsReport()
        {
            using (var db = new ApplicationDbContext())
            {
                // Предполагаем, что у вас есть статус, например "Открыта", по которому можно фильтровать
                var openStatus = db.Statuses.FirstOrDefault(s => s.StatusName == "Открыта"); //  Замените "Открыта" на фактическое название статуса в вашей БД

                if (openStatus != null)
                {
                    var openTickets = db.Tickets
                        .Include(t => t.Client)
                        .Include(t => t.Employee)
                        .Include(t => t.Status)
                        .Where(t => t.StatusId == openStatus.StatusId)
                        .ToList();

                    if (openTickets != null && openTickets.Any())
                    {
                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                        saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                        if (saveFileDialog.ShowDialog() == true)
                        {
                            string filePath = saveFileDialog.FileName;

                            try
                            {
                                using (ExcelPackage excelPackage = new ExcelPackage())
                                {
                                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Открытые заявки");

                                    // Заголовки
                                    worksheet.Cells[1, 1].Value = "ID заявки";
                                    worksheet.Cells[1, 2].Value = "Заголовок";
                                    worksheet.Cells[1, 3].Value = "Описание";
                                    worksheet.Cells[1, 4].Value = "Сотрудник";
                                    worksheet.Cells[1, 5].Value = "Клиент";
                                    worksheet.Cells[1, 6].Value = "Метод регистрации";
                                    worksheet.Cells[1, 7].Value = "Статус";
                                    worksheet.Cells[1, 8].Value = "Дата создания";

                                    // Заполнение данными
                                    for (int i = 0; i < openTickets.Count; i++)
                                    {
                                        worksheet.Cells[i + 2, 1].Value = openTickets[i].TicketId;
                                        worksheet.Cells[i + 2, 2].Value = openTickets[i].Title;
                                        worksheet.Cells[i + 2, 3].Value = openTickets[i].Description;
                                        worksheet.Cells[i + 2, 4].Value = openTickets[i].Employee?.FullName;
                                        worksheet.Cells[i + 2, 5].Value = openTickets[i].Client?.ClientName;
                                        worksheet.Cells[i + 2, 6].Value = openTickets[i].RegistrationMethod;
                                        worksheet.Cells[i + 2, 7].Value = openTickets[i].Status?.StatusName;
                                        worksheet.Cells[i + 2, 8].Value = openTickets[i].CreatedDate.ToString();
                                    }

                                    FileInfo excelFile = new FileInfo(filePath);
                                    excelPackage.SaveAs(excelFile);

                                    MessageBox.Show("Отчет по открытым заявкам успешно создан: " + filePath);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Ошибка при создании отчета: {ex.Message}");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Нет открытых заявок для отчета.");
                    }
                }
                else
                {
                    MessageBox.Show("Статус 'Открыта' не найден в базе данных.");
                }
            }
        }

        private void GenerateClosedTicketsReport()
        {
            using (var db = new ApplicationDbContext())
            {
                // Предполагаем, что у вас есть статус, например "Закрыта", по которому можно фильтровать
                var closedStatus = db.Statuses.FirstOrDefault(s => s.StatusName == "Закрыта"); // Замените "Закрыта" на фактическое название статуса в вашей БД

                if (closedStatus != null)
                {
                    var closedTickets = db.Tickets
                        .Include(t => t.Client)
                        .Include(t => t.Employee)
                        .Include(t => t.Status)
                        .Where(t => t.StatusId == closedStatus.StatusId)
                        .ToList();

                    if (closedTickets != null && closedTickets.Any())
                    {
                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                        saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                        if (saveFileDialog.ShowDialog() == true)
                        {
                            string filePath = saveFileDialog.FileName;

                            try
                            {
                                using (ExcelPackage excelPackage = new ExcelPackage())
                                {
                                    ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Закрытые заявки");

                                    // Заголовки
                                    worksheet.Cells[1, 1].Value = "ID заявки";
                                    worksheet.Cells[1, 2].Value = "Заголовок";
                                    worksheet.Cells[1, 3].Value = "Описание";
                                    worksheet.Cells[1, 4].Value = "Сотрудник";
                                    worksheet.Cells[1, 5].Value = "Клиент";
                                    worksheet.Cells[1, 6].Value = "Метод регистрации";
                                    worksheet.Cells[1, 7].Value = "Статус";
                                    worksheet.Cells[1, 8].Value = "Дата создания";

                                    // Заполнение данными
                                    for (int i = 0; i < closedTickets.Count; i++)
                                    {
                                        worksheet.Cells[i + 2, 1].Value = closedTickets[i].TicketId;
                                        worksheet.Cells[i + 2, 2].Value = closedTickets[i].Title;
                                        worksheet.Cells[i + 2, 3].Value = closedTickets[i].Description;
                                        worksheet.Cells[i + 2, 4].Value = closedTickets[i].Employee?.FullName;
                                        worksheet.Cells[i + 2, 5].Value = closedTickets[i].Client?.ClientName;
                                        worksheet.Cells[i + 2, 6].Value = closedTickets[i].RegistrationMethod;
                                        worksheet.Cells[i + 2, 7].Value = closedTickets[i].Status?.StatusName;
                                        worksheet.Cells[i + 2, 8].Value = closedTickets[i].CreatedDate.ToString();
                                    }

                                    FileInfo excelFile = new FileInfo(filePath);
                                    excelPackage.SaveAs(excelFile);

                                    MessageBox.Show("Отчет по закрытым заявкам успешно создан: " + filePath);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Ошибка при создании отчета: {ex.Message}");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Нет закрытых заявок для отчета.");
                    }
                }
                else
                {
                    MessageBox.Show("Статус 'Закрыта' не найден в базе данных.");
                }
            }
        }

        private void GenerateClientsReport()
        {
            using (var db = new ApplicationDbContext())
            {
                var clients = db.Clients.ToList();

                if (clients != null && clients.Any())
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        string filePath = saveFileDialog.FileName;

                        try
                        {
                            using (ExcelPackage excelPackage = new ExcelPackage())
                            {
                                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Клиенты");

                                // Заголовки
                                worksheet.Cells[1, 1].Value = "ID клиента";
                                worksheet.Cells[1, 2].Value = "Название клиента";
                                worksheet.Cells[1, 3].Value = "Контактное лицо";
                                worksheet.Cells[1, 4].Value = "Email";
                                worksheet.Cells[1, 5].Value = "Телефон";

                                // Заполнение данными
                                for (int i = 0; i < clients.Count; i++)
                                {
                                    worksheet.Cells[i + 2, 1].Value = clients[i].ClientId;
                                    worksheet.Cells[i + 2, 2].Value = clients[i].ClientName;
                                    worksheet.Cells[i + 2, 3].Value = clients[i].ContactPerson;
                                    worksheet.Cells[i + 2, 4].Value = clients[i].Email;
                                    worksheet.Cells[i + 2, 5].Value = clients[i].Phone;
                                }

                                FileInfo excelFile = new FileInfo(filePath);
                                excelPackage.SaveAs(excelFile);

                                MessageBox.Show("Отчет по клиентам успешно создан: " + filePath);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при создании отчета: {ex.Message}");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Нет данных для отчета по клиентам.");
                }
            }
        }

        private void GenerateEmployeesReport()
        {
            using (var db = new ApplicationDbContext())
            {
                var employees = db.Employees.ToList();

                if (employees != null && employees.Any())
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    if (saveFileDialog.ShowDialog() == true)
                    {
                        string filePath = saveFileDialog.FileName;

                        try
                        {
                            using (ExcelPackage excelPackage = new ExcelPackage())
                            {
                                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Сотрудники");

                                // Заголовки
                                worksheet.Cells[1, 1].Value = "ID сотрудника";
                                worksheet.Cells[1, 2].Value = "Логин";
                                worksheet.Cells[1, 3].Value = "ФИО";
                                worksheet.Cells[1, 4].Value = "Должность";
                                worksheet.Cells[1, 5].Value = "Роль";
                                worksheet.Cells[1, 6].Value = "Email";
                                worksheet.Cells[1, 7].Value = "Телефон";

                                // Заполнение данными
                                for (int i = 0; i < employees.Count; i++)
                                {
                                    worksheet.Cells[i + 2, 1].Value = employees[i].EmployeeId;
                                    worksheet.Cells[i + 2, 2].Value = employees[i].Login;
                                    worksheet.Cells[i + 2, 3].Value = employees[i].FullName;
                                    worksheet.Cells[i + 2, 4].Value = employees[i].Dolgnost;
                                    worksheet.Cells[i + 2, 5].Value = employees[i].Role;
                                    worksheet.Cells[i + 2, 6].Value = employees[i].Email;
                                    worksheet.Cells[i + 2, 7].Value = employees[i].Telefon;
                                }

                                FileInfo excelFile = new FileInfo(filePath);
                                excelPackage.SaveAs(excelFile);

                                MessageBox.Show("Отчет по сотрудникам успешно создан: " + filePath);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка при создании отчета: {ex.Message}");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Нет данных для отчета по сотрудникам.");
                }
            }
        }
    }
}
