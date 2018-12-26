BACKUP DATABASE SoftUni
 TO DISK = 'D:\softuni-backup.bak'
 WITH FORMAT

 DROP DATABASE SoftUni

 RESTORE DATABASE SoftUni
 FROM DISK = 'D:\softuni-backup.bak'