IF NOT EXISTS
(
	SELECT	name
	FROM	sys.objects
	WHERE	type_desc		= 'DEFAULT_CONSTRAINT'
	AND		name			= 'DF_ApplicationRouteParameter_ParameterName'
)

ALTER TABLE dbo.ApplicationRouteParameter
	ADD CONSTRAINT DF_ApplicationRouteParameter_ParameterName		DEFAULT '' 		FOR ParameterName
GO