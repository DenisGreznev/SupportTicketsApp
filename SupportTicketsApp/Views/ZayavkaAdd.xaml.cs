using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Path = System.IO.Path;

namespace SupportTicketsApp.Views
{
    /// <summary>
    /// Логика взаимодействия для ZayavkaAdd.xaml
    /// </summary>
    public partial class ZayavkaAdd : Window
    {
        public ZayavkaAdd()
        {
            InitializeComponent();
            
        }
       
        
       

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
                                                                                                                                            // ...
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            
            
        }

        private void InsertImageFromClipboardButton_Click(object sender, RoutedEventArgs e)
        {
            if (Clipboard.ContainsImage())
            {
                try
                {
                    BitmapSource bitmapSource = Clipboard.GetImage();
                    InsertImage(bitmapSource);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при вставке из буфера обмена: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("В буфере обмена нет изображения.");
            }
        }

        private void InsertImageFromFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg;*.bmp)|*.png;*.jpeg;*.jpg;*.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.UriSource = new Uri(openFileDialog.FileName);
                    bitmapImage.EndInit();

                    // Отложенный вызов InsertImage через Dispatcher
                    Dispatcher.CurrentDispatcher.BeginInvoke(
                        DispatcherPriority.Loaded,
                        new Action(() => InsertImage(bitmapImage)));

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при создании изображения: {ex.Message}");
                }
            }
        }

        private void InsertImage(BitmapSource bitmapSource)
        {
            if (bitmapSource == null)
            {
                MessageBox.Show("Ошибка: bitmapSource is null");
                return;
            }

            System.Windows.Controls.Image image = new System.Windows.Controls.Image();
            image.Source = bitmapSource;
            image.Width = 200;
            image.Height = 150;

            InlineUIContainer container = new InlineUIContainer(image, OpisanieRichTextBox.CaretPosition);
            OpisanieRichTextBox.CaretPosition = OpisanieRichTextBox.CaretPosition.GetNextInsertionPosition(LogicalDirection.Forward);
            OpisanieRichTextBox.CaretPosition.Paragraph.Margin = new Thickness(0, 0, 0, 10);

            // Используем Focus для прокрутки в .NET Framework 4.8
            OpisanieRichTextBox.Focus();// Прокрутка RichTextBox к началу

        }

        private void OpisanieRichTextBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Point clickPoint = e.GetPosition(OpisanieRichTextBox);
            TextPointer textPointer = OpisanieRichTextBox.GetPositionFromPoint(clickPoint, false);

            if (textPointer == null) return;
            InlineUIContainer container = textPointer.Parent as InlineUIContainer;
            if (container == null) return;
            System.Windows.Controls.Image clickedImage = container.Child as System.Windows.Controls.Image;
            if (clickedImage == null) return;
            BitmapSource bitmapSource = clickedImage.Source as BitmapSource;
            if (bitmapSource == null) return;

            ShowImagePreview(bitmapSource);
        }

        // Метод для отображения окна предварительного просмотра
        private void ShowImagePreview(BitmapSource bitmapSource)
        {
            // Создаем новое окно
            Window previewWindow = new Window();
            previewWindow.Title = "Предварительный просмотр изображения";
            previewWindow.Width = 600;
            previewWindow.Height = 400;

            // Создаем элемент Image для отображения в окне
            System.Windows.Controls.Image previewImage = new System.Windows.Controls.Image();
            previewImage.Source = bitmapSource;
            previewImage.Stretch = Stretch.Uniform; // чтобы изображение помещалось в окно

            // Добавляем Image в окно
            previewWindow.Content = previewImage;

            // Показываем окно
            previewWindow.ShowDialog();  //  Модальное окно

            // Другой вариант - открыть изображение во внешнем просмотрщике
            // ShowImageInExternalViewer(bitmapSource);
        }


        private void ShowImageInExternalViewer(BitmapSource bitmapSource)
        {
            string tempFilePath = Path.GetTempFileName() + ".png";
            using (var fileStream = new FileStream(tempFilePath, FileMode.Create))
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                encoder.Save(fileStream);
            }

            try
            {
                Process.Start(tempFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось открыть изображение во внешнем просмотрщике: {ex.Message}");
            }
        }


    }
}
