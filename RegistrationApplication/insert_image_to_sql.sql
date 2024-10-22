declare @picture varbinary(max);

SELECT @picture = bulkcolumn
FROM OPENROWSET(BULK N'C:\Users\djali\OneDrive\Immagini\iTop Screenshot\h1.png', SINGLE_BLOB) as T1

insert into dbo.Trainers
values ('Koffi Abo', 'Akagni', 'Hello from work', 34, 'PHD', @picture)

select *
from dbo.Trainers