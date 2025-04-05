ALTER TABLE Customers ADD ProfilePic VARBINARY(MAX);

GO

INSERT INTO Customers
(FirstName, LastName, ProfilePic, Login, PasswordHash, PostalCode, Street, BuildingNo, FlatNo, City, Tin, PhoneNumber)
VALUES
  ('Szymon', 'Kamiński', NULL, 'h4jSvWJUgac', 'ol9e4PdFDe', '56-254', 'Kajakowa', '2', '2', 'Karpacz', NULL, '356816105'),
  ('Zuzanna', 'Nowicka', NULL, 'ihKdU3NKMBr', 'AvTZh3M18TrXR', '36-350', 'Małka', '6b', NULL, 'Moszczenica', NULL, '850627645'),
  ('Tomasz', 'Totenbach', NULL, '6g4i9C6VcoL', 'Tr9hhse0QD', '90-454', 'Traczy', '6', NULL, 'Gdynia', '3312935914', '592480151'),
  ('Michał', 'Gross', NULL, 'PaVcjBeQU', 'QFWrwN9TG', '14-733', 'Korfantego', '7', NULL, 'Płock', NULL, '135396680'),
  ('Iwona', 'Jachowicz', NULL, 'pR2kPnolYR', 'YiVaUoPad5a', '22-486', 'Magnolii', '6', '28', 'Lublin', NULL, '283644177'),
  ('Agnieszka', 'Gross', NULL, '65GTREU02ah', 'UcsSRKOT0', '52-046', 'Małka', '14', '9', 'Słupsk', '1308264683', '515092418'),
  ('Michał', 'Helik', NULL, 'PfTz53kb', '22CISo5Ntn', '19-715', 'Jaskra', '33', NULL, 'Kleszczów', NULL, '239291194'),
  ('Jacek', 'Kowalski', NULL, 'pmiUXEvbyiE', 'euZWGa2pXIhf', '60-230', 'Rolna', '1', NULL, 'Białka Tatrzańska', NULL, '451052046'),
  ('Joanna', 'Kazimierczak', NULL, 'RsYKIKyUHKSpG', '8lgCSivUoJ', '82-280', 'Reja', '3b', NULL, 'Opoczno', NULL, '328768864'),
  ('Wiktor', 'Nowak', NULL, 'yosrs06hOn', 'tUxf6ua6EHlU', '61-033', 'Błotna', '6', NULL, 'Bełchatów', NULL, '187506342'),
  ('Hans', 'Lewandowski', NULL, '5Fogb2LQB', 'YCglDaCr', '44-040', 'Grota - Roweckiego', '29', NULL, 'Niebo', NULL, '956551618'),
  ('Aleksandra', 'Kamińska', NULL, '6EOX2V2Cr3', 'zWV2SkJtsMs', '57-328', 'Hleny', '1d', NULL, 'Słupsk', NULL, '982912061'),
  ('Joanna', 'Filtz', NULL, 'axA0FKGNcNX', '2Ky3ctrfYgn', '28-451', 'Wandy', '92c', NULL, 'Ręczno', '3172045148', '664158534'),
  ('Filip', 'Gołąbek', NULL, '36CSMUTK6P', 'BlSHaNNM1', '98-255', 'Łuczników', '14', NULL, 'Niebo', NULL, '076956919'),
  ('Dariusz', 'Mazur', NULL, 'VXr6XNi8K1', 'r9IonB8o0kG', '32-961', 'Jakuba', '77', NULL, 'Liszki', '9709875497', '352985849'),
  ('Piotr', 'Dura', NULL, 'wCD8WarDd', 'ywrOzQ15v6', '17-749', 'Conrada', '5', '76', 'Zgon', NULL, '314002070'),
  ('Dariusz', 'Kazimierczak', NULL, 'oxOlvJXVq1b', '7mpFAF1YPp', '32-282', 'Piekarska', '46', NULL, 'Łódź', '6500137685', '712774608'),
  ('Adam', 'Monarek', NULL, 'hxRPrwdg', '17UvLaY9EdI', '78-227', 'Polna', '3', NULL, 'Końskie', '4056107565', '098099074');