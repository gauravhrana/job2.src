IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TimeZoneClone')
BEGIN
	PRINT 'Dropping Procedure TimeZoneClone'
	DROP  Procedure TimeZoneClone
END
GO

PRINT 'Creating Procedure TimeZoneClone'
GO

/*********************************************************************************************
**		File: 
**		Name: TimeZoneClone
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

CREATE Procedure dbo.TimeZoneClone
(
		@TimeZoneId				INT			= NULL 	OUTPUT	
	,	@ApplicationId	        INT         = NULL
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(50)						
	,	@SortOrder				INT	
	,	@TimeDifference			DECIMAL(4,2)							
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'TimeZone'
)
AS
BEGIN

	IF @TimeZoneId IS NULL OR @TimeZoneId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TimeZoneId OUTPUT
	END						
	
	SELECT	@ApplicationId		= ApplicationId
		,	@Description		= Description
		,	@SortOrder			= SortOrder	
		,	@TimeDifference		= TimeDifference			
	FROM	dbo.TimeZone
	WHERE   TimeZoneId				= @TimeZoneId
	ORDER BY TimeZoneId

	EXEC dbo.TimeZoneInsert 
			@TimeZoneId	=	NULL
		,   @ApplicationId			=   ApplicationId
		,	@Name					=	@Name
		,	@Description			=	@Description
		,	@SortOrder				=	@SortOrder	
		,	@TimeDifference			=	@TimeDifference
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'TimeZone'
		,	@EntityKey				= @TimeZoneId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
