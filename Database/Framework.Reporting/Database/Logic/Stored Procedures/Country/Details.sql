IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'CountryDetails')
BEGIN
  PRINT 'Dropping Procedure CountryDetails'
  DROP  Procedure CountryDetails
END

GO

PRINT 'Creating Procedure CountryDetails'
GO


/******************************************************************************
**		File: 
**		Name: CountryDetails
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

CREATE Procedure dbo.CountryDetails
(
		@CountryId					INT					
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'Country'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@CountryId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT

	SELECT	a.CountryId			
		,	a.ApplicationId
		,	a.Name						
		,	a.Description			
		,	a.SortOrder	
		,	a.TimeZoneId	
		,	b.Name					AS	'TimeZone'
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'		
	FROM		dbo.Country		a
	INNER JOIN	Location.dbo.TimeZone	b	ON	a.TimeZoneId	=	b.TimeZoneId
	WHERE	CountryId = @CountryId	
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'Country'
		,	@EntityKey				= @CountryId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END
GO
   