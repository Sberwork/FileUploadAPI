# FileUploadAPI

## Задание. API для работы с материалами

Материал – это документ с поддержкой версионирования. 

Материал состоит из одной или множества версий. 
Каждый материал обязательно имеет категорию, которая указывается пользователем при его создании. 

Возможные категории:
•	Презентация
•	Приложение
•	Другое

Версия материала – это файл. Однажды загруженный, он не изменяем. 
При загрузке версии присваивается порядковый номер, начиная с единицы. 
По каждой версии необходимо фиксировать некоторые метаданные:

•	Когда версия была загружена (дата и время)

•	Размер загруженного файла
 
Необходимо спроектировать и реализовать следующие кейсы:

- Загрузка файла на сервер как нового материала.

- Загрузка файла на сервер в качестве новой версии существующего материала.

- Получение списка информации по материалам с фильтрацией

- Получение информации о материале

- Скачивание актуальной версии материала

- Скачивание отдельной версии материала.

- Изменение категории материалов

Кейсы должны быть реализованы в виде WebApi, желательно в виде REST-сервиса.

Реализовать постоянное хранение материалов в БД (метаданных), используя Entity Framework.

Файлы (блобы) должны храниться в локальном каталоге на сервере, который должен быть задан через web.config. 

При этом известно, что хранилище в будущем может быть изменено на FTP или даже на интеграцию с 3d party-сервисом по http. 

Необходимо иметь гибкий механизм, который позволит выполнить переход с минимальными трудозатратами.

Ограничений по расширениям файлов нет, по размеру – не более 2ГБ.

Реализованный API необходимо покрыть тестами. Unit/не Unit, а так же степень покрытия – на усмотрение кандидата.

________________________________________________________________________

### Task. API for working with materialsA material is a versioning-enabled document. 

The material consists of one or more versions. 
Each material must have a category that is specified by the user when creating it. 
Possible categories•
* Presentation
•Application
•ElseThe material version is a file.

Once uploaded, it is not changeable. 
When loading a version, it is assigned an ordinal number, starting with one. Some metadata must be recorded for each version:

•When the version was uploaded (date and time)

•Size of the uploaded file

You need to design and implement the following cases:Uploading a file to the server as a new material.
Uploading a file to the server as a new version of existing content.
Getting a list of information about filtered materialsGetting information about the materialDownload the current version of the materialDownload a separate version of the material.
Changing the content categoryCases should be implemented as a WebApi, preferably as a REST service.
Implement permanent storage of materials in the database (metadata) using the Entity Framework.
Files (blobs) must be stored in a local directory on the server, which must be set via web.config. 
At the same time, it is known that the storage may be changed to FTP in the future, or even to integration with the 3d party service via http. 
You need to have a flexible mechanism that allows you to make the transition with minimal effort.
There are no restrictions on file extensions, and the maximum size is 2 GB.The implemented API must be covered with tests. 
Unit/not Unit, as well as the degree of coverage – at the discretion of the candidate.



