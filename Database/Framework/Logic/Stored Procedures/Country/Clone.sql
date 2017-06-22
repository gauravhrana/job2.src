IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'CountryClone')
BEGIN
	PRINT 'Dropping Procedure CountryClone'
	DROP  Procedure CountryClone
END
GO

PRINT 'Creating Procedure CountryClone'
GO

/*********************************************************************************************
**		File: 
**		Name: CountryClone
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
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.CountryClone
(
		@CountryId				INT			= NULL 	OUTPUT	
	,	@ApplicationId	        INT         = NULL
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(50)						
	,	@SortOrder				INT	
	,	@TimeZoneId				INT
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'Country'
)
AS
BEGIN

	IF @CountryId IS NULL OR @CountryId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @CountryId OUTPUT
	END						
	
	SELECT	@ApplicationId		= ApplicationId
		,	@Description		= Description
		,	@SortOrder			= SortOrder	
		,	@TimeZoneId		= TimeZoneId			
	FROM	dbo.Country
	WHERE   CountryId				= @CountryId
	ORDER BY CountryId

	EXEC dbo.CountryInsert 
			@CountryId	=	NULL
		,   @ApplicationId			=   ApplicationId
		,	@Name					=	@Name
		,	@Description			=	@Description
		,	@SortOrder				=	@SortOrder	
		,	@TimeZoneId			=	@TimeZoneId
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'Country'
		,	@EntityKey				= @CountryId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
