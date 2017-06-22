IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AccidentPlaceClone')
BEGIN
	PRINT 'Dropping Procedure AccidentPlaceClone'
	DROP  Procedure AccidentPlaceClone
END
GO

PRINT 'Creating Procedure AccidentPlaceClone'
GO

/*********************************************************************************************
**		File: 
**		Name: AccidentPlaceClone
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
**     ----------							-----------
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

CREATE Procedure dbo.AccidentPlaceClone
(
		@AccidentPlaceId		INT				= NULL 	OUTPUT		
	,	@ApplicationId			INT		
	,	@Name					VARCHAR(50)							
	,	@Description            VARCHAR (500)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME		= NULL				
	,	@SystemEntityType		VARCHAR(50)		= 'AccidentPlace'				
)
AS
BEGIN

	IF @AccidentPlaceId IS NULL OR @AccidentPlaceId = -9999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @AccidentPlaceId OUTPUT
	END	
		
	
	SELECT	@ApplicationId		= ApplicationId
		,	@Name				= Name
		,	@Description		= Description
		,	@SortOrder			= SortOrder				
	FROM	dbo.AccidentPlace
	WHERE	AccidentPlaceId	= @AccidentPlaceId 
	AND		ApplicationId	= @ApplicationId

	EXEC dbo.AccidentPlaceInsert 
			@AccidentPlaceId	=	@AccidentPlaceId
		,	@ApplicationId		=	@ApplicationId
		,	@Name				=	@Name
		,	@Description		=	@Description
		,	@SortOrder			=	@SortOrder
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert	
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @AccidentPlaceId
		,	@AuditAction			= 'Clone' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
