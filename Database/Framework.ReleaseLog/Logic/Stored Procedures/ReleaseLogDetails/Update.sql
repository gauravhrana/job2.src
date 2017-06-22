IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogDetailUpdate')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogDetailUpdate'
	DROP  Procedure  ReleaseLogDetailUpdate
END
GO

PRINT 'Creating Procedure ReleaseLogDetailUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ReleaseLogDetailUpdate
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

CREATE Procedure dbo.ReleaseLogDetailUpdate
(
		@ReleaseLogDetailId				INT 			
	,	@ReleaseLogId					INT		 			
	,	@ItemNo							INT					
	,	@Description					VARCHAR(50)			
	,	@SortOrder						INT					
	,	@RequestedBy					VARCHAR(50)		    
	,	@PrimaryDeveloper				VARCHAR(50)		    
	,	@RequestedDate					INT					
	,	@AuditId						INT					
	,	@AuditDate						DATETIME	= NULL	
	,	@SystemEntityType				VARCHAR(50)	= 'ReleaseLogDetails'
)
AS
BEGIN 

	UPDATE	dbo.ReleaseLogDetail 
	SET		ReleaseLogId			= @ReleaseLogId						
	    ,   ItemNo					= @ItemNo				
		,	Description				= @Description						
		,	SortOrder				= @SortOrder			
		,	RequestedBy				= @RequestedBy			
		,	PrimaryDeveloper        = @PrimaryDeveloper		
		,	RequestedDate			= @RequestedDate				
	WHERE	ReleaseLogDetailId		= @ReleaseLogDetailId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @ReleaseLogDetailId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO