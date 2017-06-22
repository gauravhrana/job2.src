IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BathroomDelete')
BEGIN
	PRINT 'Dropping Procedure BathroomDelete'
	DROP  Procedure  BathroomDelete
END
GO

PRINT 'Creating Procedure BathroomDelete'
GO

/******************************************************************************
**		File: 
**		Name: BathroomDelete
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
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.BathroomDelete
(
	    @BathroomId	        INT
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME	= NULL
	,	@SystemEntityType	VARCHAR(50)	= 'Bathroom'	
)
AS
BEGIN
	DELETE	dbo.Bathroom
	WHERE	BathroomId = @BathroomId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @BathroomId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
GO

