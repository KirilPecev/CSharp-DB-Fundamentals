ALTER TABLE Users
ADD CONSTRAINT [Password] CHECK(LEN([Password]) >= 5);
