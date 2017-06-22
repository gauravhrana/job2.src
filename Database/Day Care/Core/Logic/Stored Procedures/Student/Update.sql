IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'StudentUpdate')
BEGIN
	PRINT 'Dropping Procedure StudentUpdate'
	DROP  Procedure  StudentUpdate
END
GO

PRINT 'Creating Procedure StudentUpdate'

GO

/******************************************************************************
**		File: 
**		Name: StudentUpdate
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

CREATE Procedure dbo.StudentUpdate
(      
		@StudentId		    INT  		 		     
	,	@LastName			VARCHAR(50)
	,	@FirstName		    VARCHAR(50)
	,   @AuditId            INT		
    ,   @AuditDate          DATETIME	= NULL 
	,   @SystemEntityType	VARCHAR(50)	= 'Student'	
)
AS
BEGIN

	UPDATE	dbo.Student 
	SET		LastName			= @LastName		 
		,	FirstName			= @FirstName	
	WHERE	StudentId		    = @StudentId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @StudentId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
GO

