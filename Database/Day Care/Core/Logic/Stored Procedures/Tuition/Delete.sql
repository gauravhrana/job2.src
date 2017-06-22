IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TuitionDelete')
BEGIN
	PRINT 'Dropping Procedure TuitionDelete'
	DROP  Procedure  TuitionDelete
END

GO

PRINT 'Creating Procedure TuitionDelete'
GO

/******************************************************************************
**		File: 
**		Name: TuitionDelete
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

CREATE Procedure dbo.TuitionDelete
(
		@TuitionId			INT
	,	@ApplicationId		INT
	,   @AuditId			INT		
    ,   @AuditDate			DATETIME = NULL
	,	@SystemEntityType	VARCHAR(50)	= 'Tuition' 
)
AS
BEGIN
	
	DELETE	dbo.Tuition
	WHERE	TuitionId = @TuitionId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TuitionId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
GO

