IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AccidentReportUpdate')
BEGIN
	PRINT 'Dropping Procedure AccidentReportUpdate'
	DROP  Procedure  AccidentReportUpdate
END
GO

PRINT 'Creating Procedure AccidentReportUpdate'

GO

/******************************************************************************
**		File: 
**		Name: AccidentReportUpdate
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
CREATE Procedure dbo.AccidentReportUpdate
(      
		@AccidentReportId		INT				
	,	@StudentId				INT
	,	@Date					DATETIME
	,	@AccidentPlaceId		INT
	,	@TeacherId				INT
	,	@Description			VARCHAR(500)	= NULL
	,	@Remedy					VARCHAR(200)	= NULL
	,	@SignoffParent			BIT
	,	@SignoffTeacher			BIT
	,	@SignoffAdmin			BIT
	,   @AuditId				INT			 
    ,   @AuditDate				DATETIME	= NULL
	,	@SystemEntityType		VARCHAR(50)	= 'AccidentReport'	
)
AS
BEGIN
	UPDATE	dbo.AccidentReport
	SET		StudentId					= @StudentId				   
		,	Date						= @Date						
		,	AccidentPlaceId				= @AccidentPlaceId				
		,	TeacherId                   = @TeacherId			    
		,	Description                 = @Description              
		,	Remedy                      = @Remedy					
		,	SignoffParent               = @SignoffParent			
		,	SignoffTeacher              = @SignoffTeacher			
		 ,  SignoffAdmin                = @SignoffAdmin    
		WHERE	AccidentReportId		= @AccidentReportId	

		--Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @AccidentReportId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO

