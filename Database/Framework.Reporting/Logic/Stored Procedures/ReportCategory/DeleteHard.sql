IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReportCategoryDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ReportCategoryDeleteHard'
	DROP  Procedure ReportCategoryDeleteHard
END
GO

PRINT 'Creating Procedure ReportCategoryDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: ReportCategoryDelete
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
CREATE Procedure dbo.ReportCategoryDeleteHard
( 
			@ReportCategoryId		INT				= NULL		
		,	@KeyId 					INT						
		,	@KeyType				VARCHAR(50)				
		,	@AuditId				INT							
		,	@AuditDate				DATETIME		= NULL
		,	@SystemEntityType		VARCHAR(50)		= 'ReportCategory'
)
AS
BEGIN

	IF @KeyType = 'ReportCategoryId'
		BEGIN

		EXEC dbo.ReportCategoryDeleteHard
				@KeyId		=	@KeyId 
			,	@KeyType	=	'ReportCategoryId'
			,	@AuditId	=	@AuditId

			DELETE	 dbo.ReportCategory
			WHERE	 ReportCategoryId = @KeyId

		END
	
END
GO
