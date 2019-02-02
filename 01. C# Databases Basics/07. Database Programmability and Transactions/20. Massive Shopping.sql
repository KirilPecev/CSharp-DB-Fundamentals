BEGIN TRANSACTION
DECLARE @level11_12ItemsPrice DECIMAL(18,2) = (SELECT SUM(Price) FROM Items WHERE MinLevel in (11,12))
DECLARE @availableCash DECIMAL(18,2) = (SELECT Cash FROM UsersGames WHERE GameId = 87)

-- Level 11 and 12
IF ((@availableCash - @level11_12ItemsPrice) >= 0)
	BEGIN

	-- taking the cash out
		update UsersGames
		set Cash -= @level11_12ItemsPrice
		where Id = 110

		if (@@ROWCOUNT <> 1)
			begin
				rollback
				return
			end
		-- inserting the items into the game
		insert into UserGameItems (UserGameId, ItemId) 
		select 110,i.Id 
		from Items as i
		where MinLevel in (11,12)
		
				if (@@ROWCOUNT <> (select count(Price) from Items where MinLevel in (11,12)))
			begin
				rollback
				return
			end
			commit
		end else 
			rollback
-- if the COMMIT statement is here, it doesnt works



begin transaction
declare @availableCash2 money = (select Cash from UsersGames where GameId = 87)
declare @level19_21ItemsPrice money = (select sum(Price) from Items where MinLevel in (19,20,21))
-- Level 19,20 and 21
	if ((@availableCash2 - @level19_21ItemsPrice) >=0)
	begin

	-- taking the cash out
		update UsersGames
		set Cash -= @level19_21ItemsPrice
		where Id = 110

		if (@@ROWCOUNT <> 1)
			begin
				rollback
				return
			end
		-- inserting the items into the game
		insert into UserGameItems (UserGameId, ItemId) select 110,i.Id from Items as i where MinLevel in (19,20,21)
		select * from UserGameItems order by UserGameId
		select * from Games
				if (@@ROWCOUNT <> (select count(Price) from Items where MinLevel in (19,20,21)))
			begin
				rollback
				return
			end
			commit	
	end else 
	begin
		rollback
	end


select i.Name AS 'Item Name' from UserGameItems as ugi
inner join Items as i
on ugi.ItemId = i.Id
where UserGameId = 110
order by i.Name