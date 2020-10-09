select * from Forums

insert into Forums (Created, Title, [Description], ImageUrl)
values
(GETDATE() , 'Ruby', 'Modern programming language from Japan', 'images/forum/rb.png'),
(GETDATE() , 'C#', 'Objectoriented programming language from Microsoft', 'images/forum/cs.png')