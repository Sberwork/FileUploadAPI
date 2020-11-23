# FileUploadAPI

Task. API for working with materialsA material is a versioning-enabled document. The material consists of one or more versions. Each material must have a category that is specified by the user when creating it. Possible categories• * Presentation•Application•ElseThe material version is a file. Once uploaded, it is not changeable. When loading a version, it is assigned an ordinal number, starting with one. Some metadata must be recorded for each version:•When the version was uploaded (date and time)•Size of the uploaded file
You need to design and implement the following cases:Uploading a file to the server as a new material.Uploading a file to the server as a new version of existing content.Getting a list of information about filtered materialsGetting information about the materialDownload the current version of the materialDownload a separate version of the material.Changing the content categoryCases should be implemented as a WebApi, preferably as a REST service.Implement permanent storage of materials in the database (metadata) using the Entity Framework.Files (blobs) must be stored in a local directory on the server, which must be set via web.config. At the same time, it is known that the storage may be changed to FTP in the future, or even to integration with the 3d party service via http. You need to have a flexible mechanism that allows you to make the transition with minimal effort.There are no restrictions on file extensions, and the maximum size is 2 GB.The implemented API must be covered with tests. Unit/not Unit, as well as the degree of coverage – at the discretion of the candidate.
upload to local folder

& to save into sql server locally

#Task: Need to merge both methods
