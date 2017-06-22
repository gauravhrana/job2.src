IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogUpdate')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogUpdate'
	DROP  Procedure  ReleaseLogUpdate
END
GO

PRINT 'Creating Procedure ReleaseLogUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ReleaseLogUpdate
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

CREATE Procedure dbo.ReleaseLogUpdate
(
		@ReleaseLogId			INT		
	,	@ReleaseLogStatusId			INT		 				 			
	,	@Name					VARCHAR(50)				
	,	@VersionNo              VARCHAR(50)         
	,	@ReleaseDate            DATETIME            
	,	@Description			VARCHAR(50)			
	,	@SortOrder				INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'ReleaseLog'
)
AS
BEGIN 

	UPDATE	dbo.ReleaseLog
	SET		ReleaseLogStatusId = @ReleaseLogStatusId
		,	Name			=	@Name			
	    ,   VersionNo       =	@VersionNo		
		,	ReleaseDate     =	@ReleaseDate		
		,	Description		=	@Description				
		,	SortOrder		=	@SortOrder							
	WHERE	ReleaseLogId	=	@ReleaseLogId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @ReleaseLogId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO