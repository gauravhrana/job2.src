IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AccidentPlaceUpdate')
BEGIN
	PRINT 'Dropping Procedure AccidentPlaceUpdate'
	DROP  Procedure  AccidentPlaceUpdate
END
GO

PRINT 'Creating Procedure AccidentPlaceUpdate'

GO

/******************************************************************************
**		File: 
**		Name: AccidentPlaceUpdate
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
**		----------						-----------
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

CREATE Procedure dbo.AccidentPlaceUpdate
(          
		@AccidentPlaceId		INT			
	,	@Name				    VARCHAR(50)
	,	@Description		    VARCHAR(500)	= NULL
	,	@SortOrder			    INT				= 1
	,   @AuditId				INT				
	,   @AuditDate				DATETIME		= NULL
	,	@SystemEntityType		VARCHAR(50)		= 'AccidentPlace'
)
AS
BEGIN
	UPDATE	dbo.AccidentPlace
	SET		Name					= @Name				      
		,	Description             = @Description         
		,	SortOrder				= @SortOrder
	WHERE	AccidentPlaceId		    = @AccidentPlaceId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @AccidentPlaceId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO

