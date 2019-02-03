--Get GameId of Alex
DECLARE @AlexUserGameId INT=
(
    SELECT Id
    FROM UsersGames AS ug
    WHERE ug.GameId =
    (
        SELECT Id
        FROM Games
        WHERE Name = 'Edinburgh'
    )
          AND ug.UserId =
    (
        SELECT Id
        FROM Users
        WHERE Username = 'Alex'
    )
)

-- Get total price of Alex's items
DECLARE @AlexItemsPrice DECIMAL(18, 2)=
(
    SELECT SUM(Price)
    FROM Items
    WHERE Name IN('Blackguard', 'Bottomless Potion of Amplification', 'Eye of Etlich (Diablo III)', 'Gem of Efficacious Toxin', 'Golden Gorget of Leoric', 'Hellfire Amulet')
)

-- Get current GameId
DECLARE @GameID INT=
(
    SELECT GameId
    FROM UsersGames
    WHERE Id = @AlexUserGameId
)

-- Insert into table UserGamesItems new items of Alex
INSERT INTO UserGameItems
       SELECT i.Id, 
              @AlexUserGameId
       FROM Items AS i
       WHERE i.[Name] IN('Blackguard', 'Bottomless Potion of Amplification', 'Eye of Etlich (Diablo III)', 'Gem of Efficacious Toxin', 'Golden Gorget of Leoric', 'Hellfire Amulet')

-- Update Cash column in UsersGames table
UPDATE UsersGames
  SET 
      Cash = Cash - @AlexItemsPrice
WHERE Id = @AlexUserGameId

-- Select all users with theirs items 
SELECT u.Username, 
       g.Name, 
       ug.Cash, 
       i.Name AS [Item Name]
FROM Users AS u
     JOIN UsersGames AS ug ON ug.UserId = u.Id
     JOIN Games AS g ON g.Id = ug.GameId
     JOIN UserGameItems AS ugi ON ugi.UserGameId = ug.Id
     JOIN Items AS i ON i.Id = ugi.ItemId
WHERE ug.GameId = @GameId
ORDER BY [Item Name]