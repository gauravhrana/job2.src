IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AccidentPlaceInsert')
BEGIN
	PRINT 'Dropping Procedure AccidentPlaceInsert'
	DROP PROCEDURE AccidentPlaceInsert
END
GO

PRINT 'Creating Procedure AccidentPlaceInsert'
GO

/******************************************************************************
**		File: 
**		Name: AccidentPlaceInsert
**		Desc: 
**
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**		----------						-----------
**
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:			Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/
CREATE PROCEDURE dbo.AccidentPlaceInsert
(
		@AccidentPlaceId		INT				= NULL 	OUTPUT	
	,	@ApplicationId			INT	
	,	@Name					VARCHAR(50)
	,	@Description			VARCHAR(500)	= NULL
	,	@SortOrder				INT				= 1
	,   @AuditId				INT						
    ,   @AuditDate				DATETIME		= NULL	
	,	@SystemEntityType		VARCHAR(50)		= 'AccidentPlace'
)
AS
BEGIN	

	
			EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @AccidentPlaceId OUTPUT
		
	INSERT INTO dbo.AccidentPlace
	(
			AccidentPlaceId
		,	ApplicationId	
		,	Name
		,	Description
		,	SortOrder
	)
	VALUES
	(
			@AccidentPlaceId
		,	@ApplicationId	
		,	@Name
		,	@Description
		,	@SortOrder
	)
	--Create Audit Record
	
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @AccidentPlaceId
		,	@AuditAction			= 'Insert' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

	
END
GO
