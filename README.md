# EventPlanning

В качестве фронт части - React + auntdesign  
Для SMTP сервера использовал - https://github.com/mailhog/MailHog  

Для запуска = применить миграции "Update-Database", запустить или mailhog или другой smtp сервер(в таком случае поменять конфиг в EmailService)  


Написать web-приложение Event planning.  
Есть административная часть и пользовательская часть.  
Функции административной части:  
Пользователь создает мероприятие произвольного типа с произвольным набором полей на определенную дату и время.  
 (Например [Наименование: тестовое мероприятие 1, тема: Спорт, местоположение: Кальварийская 7],  
либо [Наименование: тестовое мероприятие 2, Дресс-код: обязателен, тема: Музыка, подтема: Рок, подтема: Классическая музыка, участники: Певцы, деревня: Иваново])  
Функции пользовательской части:  
Пользователь может зарегистрироваться на данное мероприятие.  
Выполнить задание можно на различные уровни:  
Уровень junior:  
·         Зарегистрироваться можно посредством подтверждения ссылки на email  
Уровень middle:  
·         включает junior плюс  
·         Количество участников ограничено создателем мероприятия  
·         При входе в систему необходимо дополнить информацию с дополнительными полями о себе  
Уровень senior:  
·         включает  middle плюс  
·         Применить нестандартный метод регистрации, например, с помощью ЭЦП (или смс)  
·         При входе в систему необходимо дополнить информацию с прикреплением анкеты о себе cвозможностью парсинга анкеты (формат xls, либо pdf)  
Уровень lead:  
·         включает senior плюс  
·         Количество пользователей большое и возрастает в моменты пиков, например 5 уникальных пользователей в секунду, 100 пользователей в минуту.  
·         Предложить решение по оптимизации и построении архитектуры приложения для достижения приемлемого времени отклика страниц ( < 1 сек )  
 Единственное требования к приложению:  
ASP.NET CORE 3.1 либо выше. Ограничений по использованию Javascript-фреймворков нет.  
Ограничение по использованию БД нет, но данные должны сохраняться в БД.  
