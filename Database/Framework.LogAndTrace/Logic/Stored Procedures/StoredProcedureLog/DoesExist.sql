IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='StoredProcedureLogDoesExist')
BEGIN
	PRINT 'Dropping Procedure StoredProcedureLogDoesExist'
	DROP  Procedure  StoredProcedureLogDoesExist
END
GO

PRINT 'Creating Procedure StoredProcedureLogDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: StoredProcedureLogDoesExist
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
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

Create procedure dbo.StoredProcedureLogDoesExist
(
		@Name					VARCHAR(50)	= NULL		
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'StoredProcedureLog'
)
AS
BEGIN	

	SELECT		a.*
	FROM		dbo.StoredProcedureLog a
	WHERE		a.Name = @Name	

	


END
GO

