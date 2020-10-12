select * from Posts

insert into Posts (Content, Created,ForumId, Subject ,UserId)
values ('First Ruby post -hello!', GETDATE(), 1,'First ruby post','3ff59e0c-0740-473c-936f-383c094df6df');

select * from AspNetUsers

select p.Title as PostName, f.Title as ForumName from Posts p
inner join Forums f ON p.ForumId = f.Id
inner join AspNetUsers u ON u.Id = p.UserId
WHERE p.Id = 1