IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'CountryList')
BEGIN
	PRINT 'Dropping Procedure CountryList'
	DROP  Procedure  dbo.CountryList
END
GO

PRINT 'Creating Procedure CountryList'
GO

/******************************************************************************
**		File: 
**		Name: CountryList
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
**     ----------					   ---------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.CountryList
(
		@AuditId				INT	
	,	@ApplicationId			INT			
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'Country'
)
AS
BEGIN

	SELECT	a.CountryId			
		,	a.ApplicationId
		,	a.Name						
		,	a.Description			
		,	a.SortOrder	
		,	a.TimeZoneId	
		,	b.Name					AS	'TimeZone'
	FROM		dbo.Country		a
	INNER JOIN	Location.dbo.TimeZone	b	ON	a.TimeZoneId	=	b.TimeZoneId
	WHERE	a.ApplicationId	=	@ApplicationId

	ORDER BY CountryId			ASC
		,	 SortOrder						ASC

-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO