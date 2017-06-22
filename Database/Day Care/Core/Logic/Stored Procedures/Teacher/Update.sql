IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TeacherUpdate')
BEGIN
	PRINT 'Dropping Procedure TeacherUpdate'
	DROP  Procedure  TeacherUpdate
END
GO

PRINT 'Creating Procedure TeacherUpdate'

GO

/******************************************************************************
**		File: 
**		Name: TeacherUpdate
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

CREATE Procedure dbo.TeacherUpdate
(      
		@TeacherId		    INT	
	,	@LastName			VARCHAR(50)
	,	@FirstName		    VARCHAR(50)
	,   @AuditId            INT		
    ,   @AuditDate          DATETIME	= NULL	
	,   @SystemEntityType	VARCHAR(50)	= 'Teacher'
)
AS
 BEGIN
	UPDATE	dbo.Teacher
	SET		TeacherId              = @TeacherId 	
		,	LastName			   = @LastName		  
		,	FirstName              = @FirstName 
	WHERE	TeacherId		       = @TeacherId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TeacherId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
 END
GO

