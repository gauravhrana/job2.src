IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TimeZoneChildrenGet')
BEGIN
	PRINT 'Dropping Procedure TimeZoneChildrenGet'
	DROP  Procedure TimeZoneChildrenGet
END
GO

PRINT 'Creating Procedure TimeZoneChildrenGet'
GO


/******************************************************************************
**		File: 
**		Name: TimeZoneChildrenGet
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.TimeZoneChildrenGet
(
		@TimeZoneId				INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL   
	,	@SystemEntityType		VARCHAR(50) = 'TimeZone'
)
AS
BEGIN

	-- GET Country Records
	SELECT	a.CountryId			
		,	a.ApplicationId
		,	a.Name						
		,	a.Description			
		,	a.SortOrder	
		,	a.TimeZoneId	
		,	b.Name					AS	'TimeZone'
	FROM		Configuration.dbo.Country		a
	INNER JOIN	dbo.TimeZone	b		
		ON	a.TimeZoneId	=	b.TimeZoneId
	WHERE	a.TimeZoneId = @TimeZoneId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TimeZoneId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
   