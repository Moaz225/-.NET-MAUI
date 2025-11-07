using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace QuestionnaireApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void OnAgeValueChanged(object sender, ValueChangedEventArgs e)
        {
            int age = (int)e.NewValue;
            UpdateAgeDisplay(age);
        }

        private void OnIncrementAge(object sender, System.EventArgs e)
        {
            int currentAge = (int)AgeSlider.Value;
            if (currentAge < AgeSlider.Maximum)
            {
                AgeSlider.Value = currentAge + 1;
            }
        }

        private void OnDecrementAge(object sender, System.EventArgs e)
        {
            int currentAge = (int)AgeSlider.Value;
            if (currentAge > AgeSlider.Minimum)
            {
                AgeSlider.Value = currentAge - 1;
            }
        }

        private void UpdateAgeDisplay(int age)
        {
            AgeValueSpan.Text = age.ToString();
            AgeStepperLabel.Text = age.ToString();
        }

        private void OnPhoneTextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (Entry)sender;
            
            if (e.NewTextValue?.Length == 11)
            {
                entry.TextColor = Colors.Green;
            }
            else
            {
                entry.TextColor = Colors.Red;
            }
        }

        private void OnGenderCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            // RadioButtons в .NET MAUI автоматически обеспечивают выбор только одного значения
        }

        private void OnAgreementToggled(object sender, ToggledEventArgs e)
        {
            UpdateSubmitButtonState();
        }

        private void OnAgreementLabelTapped(object sender, System.EventArgs e)
        {
            // Переключаем Switch при нажатии на label
            AgreementSwitch.IsToggled = !AgreementSwitch.IsToggled;
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            // Можно добавить дополнительную валидацию при необходимости
        }

        private void UpdateSubmitButtonState()
        {
            bool canSubmit = AgreementSwitch.IsToggled;
            
            SubmitButton.IsEnabled = canSubmit;
            SubmitButton.BackgroundColor = canSubmit ? Color.FromArgb("#2E86AB") : Color.FromArgb("#95A5A6");
        }

        private async void OnSubmitClicked(object sender, System.EventArgs e)
        {
            // Проверка обязательных полей
            if (string.IsNullOrWhiteSpace(LastNameEntry.Text) ||
                string.IsNullOrWhiteSpace(FirstNameEntry.Text) ||
                string.IsNullOrWhiteSpace(PhoneEntry.Text) ||
                (!MaleRadioButton.IsChecked && !FemaleRadioButton.IsChecked))
            {
                await DisplayAlert("Ошибка", "Пожалуйста, заполните все обязательные поля (отмечены *)", "OK");
                return;
            }

            // Проверка номера телефона
            if (PhoneEntry.Text.Length != 11)
            {
                await DisplayAlert("Ошибка", "Номер телефона должен содержать 11 цифр", "OK");
                return;
            }

            // Сбор данных
            var userData = new
            {
                LastName = LastNameEntry.Text,
                FirstName = FirstNameEntry.Text,
                MiddleName = MiddleNameEntry.Text,
                Age = (int)AgeSlider.Value,
                Phone = PhoneEntry.Text,
                Gender = MaleRadioButton.IsChecked ? "Мужской" : "Женский",
                Agreement = AgreementSwitch.IsToggled
            };

            // В реальном приложении здесь была бы отправка данных
            await DisplayAlert("Успешно", 
                $"Анкета отправлена!\n\n" +
                $"Фамилия: {userData.LastName}\n" +
                $"Имя: {userData.FirstName}\n" +
                $"Отчество: {userData.MiddleName}\n" +
                $"Возраст: {userData.Age}\n" +
                $"Телефон: {userData.Phone}\n" +
                $"Пол: {userData.Gender}", 
                "OK");

            // Очистка формы (опционально)
            // ClearForm();
        }

        private void ClearForm()
        {
            LastNameEntry.Text = string.Empty;
            FirstNameEntry.Text = string.Empty;
            MiddleNameEntry.Text = string.Empty;
            AgeSlider.Value = 25;
            PhoneEntry.Text = string.Empty;
            PhoneEntry.TextColor = Colors.Red;
            MaleRadioButton.IsChecked = false;
            FemaleRadioButton.IsChecked = false;
            AgreementSwitch.IsToggled = false;
        }
    }
}