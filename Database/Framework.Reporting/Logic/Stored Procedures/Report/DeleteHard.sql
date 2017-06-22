IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReportDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ReportDeleteHard'
	DROP  Procedure ReportDeleteHard
END
GO

PRINT 'Creating Procedure ReportDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: ReportDelete
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
CREATE Procedure dbo.ReportDeleteHard
( 
			@ReportId				INT				= NULL		
		,	@KeyId 					INT						
		,	@KeyType				VARCHAR(50)				
		,	@AuditId				INT							
		,	@AuditDate				DATETIME		= NULL
		,	@SystemEntityType		VARCHAR(50)		= 'Report'
)
AS
BEGIN

	IF @KeyType = 'ReportId'
		BEGIN

		EXEC dbo.ReportDeleteHard
				@KeyId		=	@KeyId 
			,	@KeyType	=	'ReportId'
			,	@AuditId	=	@AuditId

			DELETE	 dbo.Report
			WHERE	 ReportId = @KeyId

		END
	
END
GO
