IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogStatusUpdate')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogStatusUpdate'
	DROP  Procedure  ReleaseLogStatusUpdate
END
GO

PRINT 'Creating Procedure ReleaseLogStatusUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ReleaseLogStatusUpdate
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
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.ReleaseLogStatusUpdate
(
		@ReleaseLogStatusId		INT 			
	,	@Name					VARCHAR(50)				
	,	@Description            VARCHAR (500)			
	,	@SortOrder				INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME		= NULL	
	,	@SystemEntityType		VARCHAR(50)		= 'ReleaseLogStatus'
)
AS
BEGIN
 
 	UPDATE	dbo.ReleaseLogStatus 
	SET		Name					=	@Name				
		,	Description				=	@Description				
		,	SortOrder				=	@SortOrder						
	WHERE	ReleaseLogStatusId		=	@ReleaseLogStatusId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ReleaseLogStatus'
		,	@EntityKey				= @ReleaseLogStatusId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END		
 GO