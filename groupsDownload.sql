use [DataContextContext-20180321055800]

--drop table #tmp1
SELECT * 
INTO #TMP1
FROM [Group]
where Id = '63DBE169-40F3-49E8-9DA7-8A4A192BEDB3'
order by id

INSERT INTO #TMP1
SELECT * 
FROM [Group] G
where ParentId in ( select id from #TMP1)
and  id not in ( select id from #TMP1)
order by id

INSERT INTO #TMP1
SELECT * 
FROM [Group] G
where ParentId in ( select id from #TMP1)
and  id not in ( select id from #TMP1)
order by id

INSERT INTO #TMP1
SELECT * 
FROM [Group] G
where ParentId in ( select id from #TMP1)
and  id not in ( select id from #TMP1)
order by id

INSERT INTO #TMP1
SELECT * 
FROM [Group] G
where ParentId in ( select id from #TMP1)
and  id not in ( select id from #TMP1)
order by id

INSERT INTO #TMP1
SELECT * 
FROM [Group] G
where ParentId in ( select id from #TMP1)
and  id not in ( select id from #TMP1)
order by id

INSERT INTO #TMP1
SELECT * 
FROM [Group] G
where ParentId in ( select id from #TMP1)
and  id not in ( select id from #TMP1)
order by id


select 'INSERT INTO [GROUP] (Id, [Name], Initials, ParentId, MeasureUnit) VALUES (''' +
Convert(nvarchar(100), Id) + ''', ''' + 
[Name] + ''', ' + 
ISNULL('''' + [Initials] + '''', 'NULL') + ', ' + 
ISNULL('''' + Convert(nvarchar(100), ParentId) + '''', 'NULL') + ', ' +
ISNULL('''' + [MeasureUnit] + '''', 'NULL') + ')'
from [#TMP1]
order by [Name]
