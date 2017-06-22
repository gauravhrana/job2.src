IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AccidentPlaceList')
BEGIN
	PRINT 'Dropping Procedure AccidentPlaceList'
	DROP PROCEDURE AccidentPlaceList
END
GO

PRINT 'Creating Procedure AccidentPlaceList'
GO

/******************************************************************************
**		File: 
**		Name: AccidentPlaceList
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
CREATE Procedure dbo.AccidentPlaceList
(
		@AuditId				INT	
	,	@ApplicationId			INT			= NULL	
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'AccidentPlace'
)
AS
BEGIN
		SELECT	AccidentPlaceId	
			,	ApplicationId   
			,	Name		  	
			,	Description	   
			,	SortOrder
		FROM   dbo.AccidentPlace 
		WHERE  ApplicationId		= ISNULL(@ApplicationId, ApplicationId)
		ORDER BY AccidentPlaceId	ASC
			,	 SortOrder			ASC

	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
