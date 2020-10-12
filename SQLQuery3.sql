select * from Posts

insert into Posts (Content, Created,ForumId, Subject ,UserId)
values ('Second post', GETDATE(), 1,'another ruby post','3ff59e0c-0740-473c-936f-383c094df6df'),
		('More ruby post', GETDATE(), 1,'Third ruby post','3ff59e0c-0740-473c-936f-383c094df6df')
select * from AspNetUsers

select p.Subject as PostName, f.Title as ForumName from Posts p
inner join Forums f ON p.ForumId = f.Id
inner join AspNetUsers u ON u.Id = p.UserId
WHERE p.Id = 1