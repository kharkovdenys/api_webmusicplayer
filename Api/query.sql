CREATE TABLE Users(
   UserName NVARCHAR(255),
   Password NVARCHAR(255), 
   PRIMARY KEY (UserName)
);
CREATE TABLE Musics(
   Id   NVARCHAR(255),
   IdVideo NVARCHAR(255),
   UserName  NVARCHAR(255),
   date  NVARCHAR(255),      
   PRIMARY KEY (Id)
);