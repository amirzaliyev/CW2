CREATE UNIQUE INDEX IX_Authors_First_Last ON Authors (FirstName, LastName)
WHERE CompanyName IS NULL;

CREATE UNIQUE INDEX IX_Authors_Company ON Authors (CompanyName)
WHERE CompanyName IS NOT NULL;
