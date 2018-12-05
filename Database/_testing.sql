insert into dbo.SCORES
select
    'Powell' as username,
    2018 as score,
    getdate() as create_dtm,
    'Y' as active,
    null as misc

insert into dbo.SCORES
select
    'Yellen' as username,
    2014 as score,
    getdate() as create_dtm,
    'Y' as active,
    null as misc

insert into dbo.SCORES
select
    'Bernanke' as username,
    2006 as score,
    getdate() as create_dtm,
    'Y' as active,
    null as misc

insert into dbo.SCORES
select
    'Greenspan' as username,
    1987 as score,
    getdate() as create_dtm,
    'Y' as active,
    null as misc

insert into dbo.SCORES
select
    'Volcker' as username,
    1979 as score,
    getdate() as create_dtm,
    'Y' as active,
    null as misc

insert into dbo.SCORES
select
    'predemo' as username,
    1 as score,
    getdate() as create_dtm,
    'Y' as active,
    null as misc

insert into dbo.SCORES
select
    'apollo' as username,
    88888 as score,
    getdate() as create_dtm,
    'Y' as active,
    null as misc

