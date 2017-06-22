ALTER TABLE dbo.ApplicationRouteParameter
	ADD CONSTRAINT FK_ApplicationRouteParameter_ApplicationRoute FOREIGN KEY
	(
		ApplicationRouteId
	)
	REFERENCES dbo.ApplicationRoute
	(
		ApplicationRouteId 
	)
GO