Public Class CalculadoraForm
    Private parte_entero As String
    Private parte_decimal As String

    Private parte_actual As Integer = 0

    Private operando1 As Decimal
    Private operando2 As Decimal
    Private operador As String

    Enum PARTE_ACTUAL_OPCIONES
        _ENTERO = 0
        _PUNTO = 1
        _DECIMAL = 2
    End Enum

    Private Sub Button_digits_Click(sender As Object, e As EventArgs) Handles ButtonPunto.Click,
                                                                               Button0.Click,
                                                                               Button1.Click,
                                                                               Button2.Click,
                                                                               Button3.Click,
                                                                               Button4.Click,
                                                                               Button5.Click,
                                                                               Button6.Click,
                                                                               Button7.Click,
                                                                               Button8.Click,
                                                                               Button9.Click
        Try
            pantalla_agregar(sender.Text)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub pantalla_agregar(digito As String)
        If digito.Equals(".") Then
            If parte_actual <> PARTE_ACTUAL_OPCIONES._ENTERO Then
                Exit Sub
            End If

            parte_actual = PARTE_ACTUAL_OPCIONES._PUNTO
        Else
            If parte_actual = PARTE_ACTUAL_OPCIONES._PUNTO Then
                parte_actual = PARTE_ACTUAL_OPCIONES._DECIMAL
            End If
        End If

        Select Case parte_actual
            Case 0
                parte_entero &= digito
                PantallaTxt.Text = Val(parte_entero).ToString("#,##0")
            Case 1
                PantallaTxt.Text &= digito
            Case 2
                parte_decimal &= digito
                PantallaTxt.Text = Val(parte_entero).ToString("#,##0") & "." & parte_decimal
        End Select
    End Sub

    Private Sub ButtonOperando_Click(sender As Object, e As EventArgs) Handles ButtonMultiplicar.Click,
                                                                                ButtonDividir.Click,
                                                                                ButtonMas.Click,
                                                                                ButtonMenos.Click
        Try
            agregar_operacion(sender.Text)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.OkOnly + MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub agregar_operacion(op As Object)
        If parte_actual = PARTE_ACTUAL_OPCIONES._PUNTO Then
            PantallaTxt.Text = PantallaTxt.Text.Replace(".", "")
            parte_actual = PARTE_ACTUAL_OPCIONES._ENTERO
        End If

        operando1 = Val(PantallaTxt.Text)
        operador = op

        If parte_actual = PARTE_ACTUAL_OPCIONES._ENTERO Then
            PantallaSecundariaTXT.Text = String.Format("{0} {1}", operando1.ToString("#,##0"), operador)
            parte_entero = ""
            PantallaTxt.Clear()
        Else
            PantallaSecundariaTXT.Text = String.Format("{0}.{1} {2}", operando1.ToString("#,##0"), parte_decimal, operador)
            parte_entero = ""
            parte_decimal = ""
            PantallaTxt.Clear()
        End If

        parte_actual = PARTE_ACTUAL_OPCIONES._ENTERO
    End Sub

    Private Sub ButtonIgual_Click(sender As Object, e As EventArgs) Handles ButtonIgual.Click
        Try
            calcular_resultado()
        Catch ex As Exception
            MsgBox(ex.Message, vbOK + vbCritical)
        End Try
    End Sub

    Private Sub calcular_resultado()
        If operador.Length = 0 Then
            Exit Sub
        End If

        operando2 = Val(PantallaTxt.Text)

        PantallaTxt.Text = realizar_operacion()
        PantallaSecundariaTXT.Clear()
        parte_actual = PARTE_ACTUAL_OPCIONES._ENTERO
        parte_entero = ""
        parte_decimal = ""
    End Sub

    Private Function realizar_operacion() As String
        Dim resultado As Decimal

        Select Case operador
            Case "X"
                resultado = operando1 * operando2
            Case "/"
                resultado = operando1 / operando2
            Case "+"
                resultado = operando1 + operando2
            Case "-"
                resultado = operando1 - operando2
        End Select

        Return resultado
    End Function

    Private Sub ButtonClear_Click(sender As Object, e As EventArgs) Handles ButtonClear.Click
        parte_actual = PARTE_ACTUAL_OPCIONES._ENTERO
        parte_entero = ""
        parte_decimal = ""

        operando1 = 0
        operando2 = 0
        operador = ""
        PantallaSecundariaTXT.Clear()
        PantallaTxt.Clear()
    End Sub
End Class
